namespace Tratament.Web.ViewModels.SendRequest
{
    public class SubmitViewModel
    {
        public string Idnp { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }
        
        public string TicketTypeId { get; set; }

        public string TicketType { get; set; }

        public string RequestNumber { get; set; }

        public DateTime? RequestSubmitDate { get; set; }
    }
}
