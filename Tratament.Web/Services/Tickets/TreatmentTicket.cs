using Tratament.Model.Models.EcerereTicketService;
using Tratament.Web.LoggerSetup;
using Tratament.Web.Service.TreatmentTicket.Service;

namespace Tratament.Web.Services.Tickets
{
    public class TreatmentTicket : ITreatmentTicket
    {
        public async Task<string> InsertTicketToEcerere(TicketInsertModel ticket)
        {
            try
            {
                ins_ecerereRequest request = new ins_ecerereRequest();

                request.vpres_rf = ticket.Vpres_rf;
                request.vidnp = ticket.Vidnp;
                request.vnume = ticket.Vnume;
                request.vprenume = ticket.Vprenume;

                request.vcuatm = ticket.Vcuatm;
                request.vadresa = ticket.Vadresa;
                request.vtelefon = ticket.Vtelefon;
                request.vemail = ticket.Vemail;
                request.vnascut_d = ticket.VnascutD;
                request.vsex = ticket.Vsex;

                BiletePortTypeChannel client = TreatmentTicketClient.SetClient();

                ins_ecerereResponse response = await client.ins_ecerereAsync(request);

                string cerereId = response.Element.FirstOrDefault().cerere_id;

                return cerereId;

            }
            catch (Exception ex)
            {
                WriteLog.Common.Error("InsertTicketToEcerere error: ", ex);

                return null;
            }
        }
    }
}
