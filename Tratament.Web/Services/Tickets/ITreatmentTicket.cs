using Tratament.Model.Models.EcerereTicketService;
using Tratament.Web.ViewModels.SendRequest;

namespace Tratament.Web.Services.Tickets
{
    public interface ITreatmentTicket
    {
        public Task<string> InsertTicketToEcerere(TicketInsertModel ticket);    
    }
}
