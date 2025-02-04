using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Tratament.Web.ViewModels.SendRequest
{
    public class SendRequestViewModel
    {
        public SendRequestViewModel()
        {
            PenionType = new List<SelectListItem>();
        }

        [Required(ErrorMessage = @"Acest cîmp este obligator.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = @"IDNP-ul trebuie să fie din 13 cifre.")]
        public string Idnp { get; set; }

        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Emailul nu este valid.")]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Required(ErrorMessage = @"Acest cîmp este obligator.")]
        public string TicketTypeId { get; set; }

        public List<SelectListItem> PenionType { get; set; }
    }
}
