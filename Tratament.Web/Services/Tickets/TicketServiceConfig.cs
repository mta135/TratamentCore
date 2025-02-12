using ServiceReference;
using static ServiceReference.BiletePortTypeClient;

namespace Tratament.Web.Services.Tickets
{
    public class TicketServiceConfig
    {
        public static BiletePortTypeClient SetClient()
        {
            BiletePortTypeClient client = new(EndpointConfiguration.SOAP11Endpoint);

            return client;
        }
    }
}
