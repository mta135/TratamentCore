using System.Globalization;
using System.Text;
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

                request.vnume = RemoveDiacritics(ticket.Vnume);
                request.vprenume = RemoveDiacritics(ticket.Vprenume);

                request.vcuatm = ticket.Vcuatm;

                request.vadresa = RemoveDiacritics(ticket.Vadresa);
                
                request.vtelefon = ticket.Vtelefon;
                request.vemail = ticket.Vemail;
                request.vnascut_d = ticket.VnascutD;
                request.vsex = ticket.Vsex;

                BiletePortTypeChannel client = TreatmentTicketClient.SetClient();

                ins_ecerereResponse response = await client.ins_ecerereAsync(request);

                TempLogs(request);

                string cerereId = response.Element.FirstOrDefault().cerere_id;

                return cerereId;

            }
            catch (Exception ex)
            {
                WriteLog.Common.Error("InsertTicketToEcerere error: ", ex);

                return null;
            }
        }


        private string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            // Normalize to decomposed form (FormD)
            string normalizedText = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalizedText)
            {
                // Check if the character is a diacritic
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            // Normalize back to FormC for proper composition
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }


        #region Log

        public void TempLogs(ins_ecerereRequest request)
        {

            WriteLog.Common.Debug("InsertTicketToEcerere. Send params" + "; vpres_rf: " + request.vpres_rf + "; vidnp: "

                + request.vidnp + "; request.vnume; " +  RemoveDiacritics(request.vnume) + "; request.vprenume: " + RemoveDiacritics(request.vprenume) + ";" +
                " request.vcuatm: " + request.vcuatm + "; request.vadresa: " + RemoveDiacritics(request.vadresa) + ";" +
                " request.vtelefon: " + request.vemail + "; request.vnascut_d: " + request.vnascut_d + "; Sex: " + request.vsex);
        }

        #endregion
    }



}
