using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Tratament.Web.DocumentService.IDocumentService;
using Tratament.Web.Options;
using Tratament.Web.ViewModels.SendRequest;

namespace Tratament.Web.Controllers
{
    public class SendRequestController : Controller
    {

        private readonly IPdfGenerator pdfGenerator;

        private readonly RecaptchatOption recaptchatOption;
        private readonly RecaptchaHelper recaptchaHelper;

        public SendRequestController(IPdfGenerator pdfGenerator, IOptions<RecaptchatOption> options)
        {    
            this.pdfGenerator = pdfGenerator;
            recaptchatOption = options.Value;
            recaptchaHelper = new RecaptchaHelper(options);
        }



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

            string captchaResponse = Request.Form["g-recaptcha-response"].ToString();
            var validate = recaptchaHelper.ValidateCaptha(captchaResponse);

            if (!validate.Success)
            {
                RedirectToAction("Send", requestViewModel);
            }

            return RedirectToAction("/SendRequest/Submited", requestViewModel);
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
