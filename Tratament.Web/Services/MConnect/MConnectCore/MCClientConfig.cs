using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MAIeDosar.API.Services.MConnect
{
    public abstract class MCClientConfig
    {
        private readonly string soapNamespace;
        private readonly string contentType;
        private readonly IHttpClientFactory clientFactory;

        protected MCClientConfig(IHttpClientFactory clientFactory, string soapNamespace, string contentType)
        {
            this.soapNamespace = soapNamespace;
            this.contentType = contentType;
            this.clientFactory = clientFactory;
        }

        public  HttpRequestMessage BuildRequest(RequestSettings requestSettings)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(requestSettings.Uri.ToString()));
            FillHeaders(requestSettings.RequestHeaders, httpRequestMessage.Headers);

            httpRequestMessage.Content = new StringContent(CreateXmlMessage(requestSettings.Content, requestSettings.SignMessage), Encoding.UTF8, contentType);
            FillHeaders(requestSettings.ContentHeaders, httpRequestMessage.Content.Headers);
            return httpRequestMessage;
        }

        public async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, long timeout = 0)
        {
            var client = clientFactory.CreateClient();
            client.Timeout = TimeSpan.FromMilliseconds(timeout);
            return await client.SendAsync(request);
        }

        protected void FillHeaders(string rawHeaders, HttpHeaders headers)
        {
            if (string.IsNullOrWhiteSpace(rawHeaders))
                return;

            foreach (var rawHeader in rawHeaders.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var headerText = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(rawHeader)); // HTTP headers can have only ASCII chars
                var indexOfColon = headerText.IndexOf(':');
                if (indexOfColon <= 0) continue;
                headers.TryAddWithoutValidation(headerText.Substring(0, indexOfColon).Trim(), headerText.Substring(indexOfColon + 1).Trim());
            }
        }

        private string CreateXmlMessage(string requestBody, bool signMessage)
        {
            if (!signMessage)
            {
                return $@"<soap:Envelope xmlns:soap=""{soapNamespace}"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><soap:Body>{requestBody}</soap:Body></soap:Envelope>";
            }

            var doc = new XmlDocument();
            var envelope = doc.CreateElement("soap", "Envelope", soapNamespace);
            envelope.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            doc.AppendChild(envelope);

            // Create Security Header
            var id = Guid.NewGuid().ToString("N");
            var TimestampNs = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";

            var security = doc.CreateElement("Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            var msAtt = doc.CreateAttribute("soap", "mustUnderstand", soapNamespace);
            msAtt.InnerText = "1";
            security.Attributes.Append(msAtt);

            var timestamp = doc.CreateElement("Timestamp", TimestampNs);
            timestamp.SetAttribute("Id", "TS-" + id);
            security.AppendChild(timestamp);

            var created = doc.CreateElement("Created", TimestampNs);
            created.InnerText = XmlConvert.ToString(DateTimeOffset.UtcNow);
            timestamp.AppendChild(created);

            var expires = doc.CreateElement("Expires", TimestampNs);
            expires.InnerText = XmlConvert.ToString(DateTimeOffset.UtcNow.AddMinutes(15));
            timestamp.AppendChild(expires);

            var header = doc.CreateElement("soap", "Header", soapNamespace);
            header.AppendChild(security);
            envelope.AppendChild(header);

            // Create Body
            var body = doc.CreateElement("soap", "Body", soapNamespace);
            envelope.AppendChild(body);
            var bodyDocument = new XmlDocument();
            try
            {
                bodyDocument.LoadXml(requestBody);
            }
            catch
            {
                throw new ApplicationException("Invalid XML for SOAP Body");
            }
            body.AppendChild(body.OwnerDocument.ImportNode(bodyDocument.DocumentElement, true));
            body.SetAttribute("Id", "MS-" + id);

            return ApplySignature(doc, header, body, id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseSettings"></param>
        /// <param name="responseBody"></param>
        /// <returns>XML Документ / Первый Ребенок Body внутри XML Документа</returns>
        /// <exception cref="ApplicationException"></exception>
        public Tuple<XmlDocument, XmlNode> GetResponseBody(ResponseSettings responseSettings, string responseBody)
        {
            try
            {
                var xmlDocument = new XmlDocument
                {
                    PreserveWhitespace = true
                };
                xmlDocument.LoadXml(responseBody);

                XmlNodeList bodyNodes = xmlDocument.GetElementsByTagName("Body", soapNamespace);
                if (bodyNodes.Count != 1)
                    throw new ApplicationException("No or more than one SOAP Body in response");

                XmlNode body = bodyNodes[0];
                if (body.FirstChild == null)
                    throw new ApplicationException("No child in SOAP Body");

                if (responseSettings.MessageSigned)
                {
                    ValidateSignature(xmlDocument, responseSettings.ServiceCertificate);
                }
                //return body.FirstChild;
                //return JObject.Parse(JsonConvert.SerializeXmlNode(body.FirstChild));
                return new Tuple<XmlDocument, XmlNode>(xmlDocument, body.FirstChild);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Invalid SOAP Message in response", ex);
            }
        }

        private string ApplySignature(XmlDocument doc, XmlElement header, XmlElement body, string id)
        {
            body.SetAttribute("Id", "MS-" + id);

            var keyInfo = new KeyInfo();

            X509Certificate2 certificate = MccCertificateConfig.GetClientCerificate();

            keyInfo.AddClause(new KeyInfoX509Data(certificate));
            var signedXml = new SignedXml(doc)
            {
                KeyInfo = keyInfo,
                SigningKey = certificate.GetRSAPrivateKey()
            };

            signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
            var bodyReference = new Reference
            {
                Uri = "#MS-" + id
            };
            bodyReference.AddTransform(new XmlDsigExcC14NTransform());  // required to match doc
            signedXml.AddReference(bodyReference);

            Reference tsReference = new Reference
            {
                Uri = "#TS-" + id
            };
            tsReference.AddTransform(new XmlDsigExcC14NTransform());  // required to match doc
            signedXml.AddReference(tsReference);

            signedXml.ComputeSignature();
            var signedElement = signedXml.GetXml();

            header.FirstChild.AppendChild(signedElement);
            return doc.InnerXml;
        }

        private void ValidateSignature(XmlDocument doc, X509Certificate2 serviceCertificate)
        {
            var signatureNodes = doc.GetElementsByTagName("Signature", "http://www.w3.org/2000/09/xmldsig#");
            if (signatureNodes.Count != 1) throw new ApplicationException("No or more than one SOAP Signature in response");
            var sdoc = new SignedSoapXml(doc.DocumentElement);
            sdoc.LoadXml((XmlElement)signatureNodes[0]);
            if (!sdoc.CheckSignature(serviceCertificate, true)) throw new ApplicationException("Invalid SOAP Signature");
        }

        private class SignedSoapXml : SignedXml
        {
            public SignedSoapXml(XmlElement elem) : base(elem)
            {
            }

            public override XmlElement GetIdElement(XmlDocument document, string idValue)
            {
                var nodes = document.SelectNodes("//*[@*[local-name()='Id' and .='" + idValue + "']]");
                if ((nodes == null) || (nodes.Count != 1)) return null;
                return nodes[0] as XmlElement;
            }
        }

    }
}
