using Tratament.Model.Models.Enums;

namespace Tratament.Web.ViewModels.SendRequest.Helepr
{
    public class SubmitRequestHelper
    {
        public static string GetCompensationTypeTiket(string ticketId)
        {
            string compType = string.Empty;

            if (ticketId == "1")
                compType = TicketCompensationTypeEnum.DesabilityTicket;

            if (ticketId == "2")
                compType = TicketCompensationTypeEnum.VeteransTicket;

            if (ticketId == "3")
                compType = TicketCompensationTypeEnum.CernobilTicket;

            if (ticketId == "4")
                compType = TicketCompensationTypeEnum.CompenstaionTicket;

            return compType;
        }
    }
}
