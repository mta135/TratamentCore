using System.ServiceModel;
using Tratament.Web.Service.TreatmentTicket.Service;

namespace Tratament.Web.Services.Tickets
{
    public class TreatmentTicketClient
    {
        public static BiletePortTypeChannel SetClient()
        {
            string serviceUrl = "http://172.16.1.11:9763/services/Bilete";

            BasicHttpBinding binding = new BasicHttpBinding
            {
                Security = new BasicHttpSecurity
                {
                    Mode = BasicHttpSecurityMode.None // No HTTPS
                }
            };

            EndpointAddress endpoint = new EndpointAddress(serviceUrl);
            ChannelFactory<BiletePortTypeChannel> factory = new ChannelFactory<BiletePortTypeChannel>(binding, endpoint);

            return factory.CreateChannel();
        }
    }
}
