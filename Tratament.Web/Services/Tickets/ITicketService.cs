using Tratament.Web.ViewModels.SendRequest;

namespace Tratament.Web.Services.Tickets
{
    public interface ITicketService
    {
        public void InsertTicketToEcerere(SubmitViewModel submitViewModel);
       
    }
}
