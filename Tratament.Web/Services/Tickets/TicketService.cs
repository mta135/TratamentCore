using ServiceReference;
using System.Threading.Tasks;
using Tratament.Model.Models.EcerereTicketService;
using Tratament.Web.LoggerSetup;
using Tratament.Web.ViewModels.SendRequest;

namespace Tratament.Web.Services.Tickets
{
    public class TicketService : ITicketService
    {
        public async Task<(string cerereId, string errorNumber)> InsertTicketToEcerere(TicketInsertModel ticket)
        {
            try
            {
                BiletePortTypeClient client = TicketServiceConfig.SetClient();

                ins_ecerereResponse response = await client.ins_ecerereAsync(ticket.Vpres_rf, ticket.Vidnp, ticket.Vnume,
                    ticket.Vidnp, ticket.Vcuatm, ticket.Vadresa, ticket.Vtelefon, ticket.Vemail, ticket.VnascutD, ticket.Vsex);

                string cerereId = response.Element.FirstOrDefault().cerere_id;

                return (cerereId, null);

            }
            catch (Exception ex)
            {
                WriteLog.Common.Error("InsertTicketToEcerere error: ", ex);

                return (null, "1");
            }
        }
    }
}
