using Tratament.Model.Models.EcerereTicketService;
using Tratament.Web.ViewModels.SendRequest;

namespace Tratament.Web.Services.Tickets
{
    public interface ITicketService
    {
        public Task<string> InsertTicketToEcerere(TicketInsertModel ticket);
       
    }
}
