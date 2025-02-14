using System.ServiceModel;
using Tratament.Web.Service.TreatmentTicket.Service;

namespace Tratament.Web.Services.Tickets
{
    public class TreatmentTicketClient
    {

        private static string ServiceUrl;

        public static void InitializeSettings(IConfiguration config)
        {
            ServiceUrl = config.GetValue<string>("TreatmentTicketService:ServiceUrl");
        }

        public static BiletePortTypeChannel SetClient()
        {
            BasicHttpBinding binding = new BasicHttpBinding
            {
                Security = new BasicHttpSecurity
                {
                    Mode = BasicHttpSecurityMode.None // No HTTPS
                }
            };

            EndpointAddress endpoint = new EndpointAddress(ServiceUrl);
            ChannelFactory<BiletePortTypeChannel> factory = new ChannelFactory<BiletePortTypeChannel>(binding, endpoint);

            return factory.CreateChannel();
        }
    }
}
