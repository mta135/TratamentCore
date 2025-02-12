using Tratament.Web.ViewModels.SendRequest;

namespace Tratament.Web.Services.Tickets
{
    public class TicketService : ITicketService
    {
        public void InsertTicketToEcerere(SubmitViewModel submitViewModel)
        {
            ServiceReference.BiletePortTypeClient client = TicketServiceConfig.SetClient();
            //var value = client.ins_ecerereAsync();
        }
    }
}
