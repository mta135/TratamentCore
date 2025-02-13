using Tratament.Model.Models.Enums;

namespace Tratament.Web.ViewModels.SendRequest.Heleper
{
    public class SubmitRequestHelper
    {
        public static string GetCompensationTypeTiket(string ticketId)
        {
            string compType = string.Empty;

            if (ticketId == "1")
                compType = TicketTypeEnum.DesabilityTicket;

            if (ticketId == "2")
                compType = TicketTypeEnum.VeteransTicket;

            if (ticketId == "3")
                compType = TicketTypeEnum.CernobilTicket;

            if (ticketId == "4")
                compType = TicketTypeEnum.CompenstaionTicket;

            return compType;
        }


        public static string  NormalizeStringLength(string value, int maxLength)
        {
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}
