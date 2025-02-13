using System.Xml;
using System.Xml.Linq;

namespace Tratament.Web.Services.MConnect.MConnectCore
{
    public sealed class MCClient : MCClientConfig
    {

        public MCClient(IHttpClientFactory clientFactory) : base(clientFactory, "http://schemas.xmlsoap.org/soap/envelope/", "text/xml")
        {

        }

        public HttpRequestMessage BuildRequest(RequestSettings requestSettings)
        {
            var httpRequestMessage = base.BuildRequest(requestSettings);
            requestSettings.SoapAction = "\"" + (string.IsNullOrEmpty(requestSettings.SoapAction) ? null : requestSettings.SoapAction) + "\"";
            httpRequestMessage.Headers.Add("SOAPAction", requestSettings.SoapAction);
            return httpRequestMessage;
        }

        public new async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, long timeout = 0)
        {
            return await base.SendRequest(request, timeout);
        }

        public new Tuple<XmlDocument, XmlNode> GetResponseBody(ResponseSettings responseSettings, string responseBody)
        {
            return base.GetResponseBody(responseSettings, responseBody);
        }

        public string BuildContent(string parameter)
        {
            string result = BuildContentXML("GetPersonDataForActualization", "IDNP", parameter);

            return result;
        }

        private string BuildContentXML(string actionType, string paramName, string param)
        {
            XNamespace mcon = "http://cnas.md/";
            XElement root = new XElement(mcon + actionType,
                new XAttribute(XNamespace.Xmlns + "cnas", "http://cnas.md/"),

                new XElement(mcon + "IDNO", "1004600030235"),
                new XElement(mcon + paramName, param)
            );

            return root.ToString();
        }
    }
}

