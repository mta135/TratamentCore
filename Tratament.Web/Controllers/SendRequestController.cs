using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tratament.Web.ViewModels.SendRequest;

namespace Tratament.Web.Controllers
{
    public class SendRequestController : Controller
    {

        [HttpGet]
        public IActionResult Send()
        {
            SendRequestViewModel requestViewModel = new();

            ViewBag.TicketTypes = GetTicketTypes();


            return View(requestViewModel);
        }


        [HttpPost]
        public IActionResult Send(SendRequestViewModel requestViewModel)
        {

            ViewBag.TicketTypes = GetTicketTypes();


            return View(requestViewModel);
        }


        private List<SelectListItem> GetTicketTypes()
        {
            List<SelectListItem> pensionType = new List<SelectListItem>
            {
                new SelectListItem { Text = "Bilete pentru persoanele cu dizabilităţi", Value = "1" },
                new SelectListItem { Text = "Bilete pentru veterani", Value = "2" },
                new SelectListItem { Text = "Bilete pentru Cernobîl", Value = "3" },
                new SelectListItem { Text = "Compensaţia bănească în schimbul biletului pentru Cernobîl", Value = "4" }
            };

            return pensionType;
        }
    }
}
