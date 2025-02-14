using ServiceReference;
using static ServiceReference.BiletePortTypeClient;

namespace Tratament.Web.Services.Tickets
{
    public class TreatmentTicketClient
    {
        public static BiletePortTypeClient SetClient()
        {
            BiletePortTypeClient client = new(EndpointConfiguration.SOAP11Endpoint);

            return client;
        }
    }
}
