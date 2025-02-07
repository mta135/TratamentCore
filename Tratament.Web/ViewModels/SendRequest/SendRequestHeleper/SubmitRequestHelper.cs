using Tratament.Model.Models.Enums;

namespace Tratament.Web.ViewModels.SendRequest.Heleper
{
    public class SubmitRequestHelper
    {
        public static string GetCompensationTypeTiket(string ticketId)
        {
            string compType = string.Empty;

            if (ticketId == "1")
                compType = CompensationTypeEnum.DesabilityTicket;

            if (ticketId == "2")
                compType = CompensationTypeEnum.VeteransTicket;

            if (ticketId == "3")
                compType = CompensationTypeEnum.CernobilTicket;

            if (ticketId == "4")
                compType = CompensationTypeEnum.CompenstaionTicket;

            return compType;
        }
    }
}
