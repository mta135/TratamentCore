using Tratament.Web.Services.MConnect.MConnectCore;
using Tratament.Web.ServicesModels.PhisicalPerson;
using System.Xml;
using Tratament.Web.Services.MConnect.Models.Person;
using Tratament.Model.Models.ExternalServices;

namespace Tratament.Web.Services.MConnect
{
    public class MConnectService : IMConnectService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
 

        public MConnectService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

        }

        public async Task<PersonModel> GetPerson(PersonFilter filter)
        {
            PersonParser person = new PersonParser();

            (XmlDocument document, XmlNode node) = await GetResponse(filter.IDNP, MConnectActionType.Person);

            PersonModel personAPI = await person.GetParsedPerson(document);

            return personAPI;
        }



        /// <summary>
        /// Solid - S - Ответственность метода - построить XML по параметрам и получить XML
        /// </summary>
        /// <param name="parameter">IDNP/IDNO</param>
        /// <param name="type">Person/Org</param>
        /// <returns>XML</returns>
        private async Task<Tuple<XmlDocument, XmlNode>> GetResponse(string parameter, MConnectActionType type)
        {
            MCClient mCClient = new MCClient(_httpClientFactory);

            //Создаются параметры - для дальнейшего построения XML
            string endpointUrl = _configuration.GetValue<string>("MConnect:EndPointUrl");
            string soapAction = type == MConnectActionType.Person ? _configuration.GetValue<string>("MConnect:GetPerson")
                                                                    : _configuration.GetValue<string>("MConnect:GetLegalEntity");

            string callingUser = parameter;
            string RequestHeaders = $"CallingEntity: 1004600030235\r\nCallingUser: {callingUser} \r\nCallBasis: cnas\r\nCallReason: cnas";

            //Здесь строится XML Request в зависимости от type
            var content = mCClient.BuildContent(parameter, type);
            var request = mCClient.BuildRequest(new RequestSettings
            {
                Uri = new Uri(endpointUrl),
                RequestHeaders = RequestHeaders,
                SoapAction = soapAction,
                ContentHeaders = "",
                Content = content,
                SignMessage = true
            });

            string _sendedRequest = await request.Content.ReadAsStringAsync();
          //  MainLogger.Web.Info($"MConnect {type} Request: \n {_sendedRequest}");

            var httpResponse = await mCClient.SendRequest(request, 45000);
            string Response = await httpResponse.Content.ReadAsStringAsync();

            ResponseSettings responseSettings = new ResponseSettings
            {
                MessageSigned = true,
                ServiceCertificate = MccCertificateConfig.GetServiceCertificate()
            };

            (XmlDocument document, XmlNode node) = mCClient.GetResponseBody(responseSettings, Response);

           // MainLogger.Web.Info($"MConnect {type} Response: \n {Response}");
            return new Tuple<XmlDocument, XmlNode>(document, node);


        }
    }
}
