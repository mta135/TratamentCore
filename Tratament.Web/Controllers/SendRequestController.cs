using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tratament.Web.DocumentService.IDocumentService;
using Tratament.Web.Recaptcha.Interface;
using Tratament.Web.Recaptcha.RecaptchaHelpers;
using Tratament.Web.ViewModels.SendRequest;

namespace Tratament.Web.Controllers
{
    public class SendRequestController : Controller
    {

        private readonly IPdfGenerator pdfGenerator;

        private readonly IRecaptchaService _recaptchaService;

        public SendRequestController(IPdfGenerator pdfGenerator, IRecaptchaService recaptchaService)
        {    
            this.pdfGenerator = pdfGenerator;

            _recaptchaService = recaptchaService;
        }



        [HttpGet]
        public IActionResult Send()
        {
            SendRequestViewModel requestViewModel = new();

            ViewBag.TicketTypes = GetTicketTypes();

            return View(requestViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Send(SendRequestViewModel requestViewModel)
        {
            ViewBag.TicketTypes = GetTicketTypes();

            string captchaResponse = Request.Form["g-recaptcha-response"].ToString();

            if (!await _recaptchaService.VerifyRecaptchaAsync(captchaResponse))
            {
                RedirectToAction("Send", requestViewModel);
            }

            return RedirectToAction("Submited", "SendRequest");
        }


        public IActionResult Submited()
        {
            return View();
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
