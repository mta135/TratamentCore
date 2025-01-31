using MAIeDosar.API.ApiViewModels.ExternalServices;
using MAIeDosar.API.Services.MConnect;
using MAIeDosar.API.ServicesModels.Civil;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tratament.Web.Recaptcha.Interface;
using Tratament.Web.ViewModels.SendRequest;

namespace Tratament.Web.Controllers
{
    public class SendRequestController : Controller
    {
        private readonly IRecaptchaService _recaptchaService;

        private readonly IMConnectService _mConnectService;

        public SendRequestController( IRecaptchaService recaptchaService, IMConnectService mConnectService)
        {    
            _recaptchaService = recaptchaService;
            _mConnectService = mConnectService;
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
                ViewBag.IsVerified = true;
                return View("Send");
            }

            return RedirectToAction("Submited", "SendRequest");
        }

        [HttpGet]
        public IActionResult Submited()
        {
            SubmitViewModel submitViewModel = new SubmitViewModel();

            return View(submitViewModel);
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

        public async Task<IActionResult> TestMconnect()
        {
            PersonFilter personFilter = new PersonFilter();
            personFilter.IDNP = "2004023011612";

            PersonAPIModel personAPI = await _mConnectService.GetPerson(personFilter);
            var a = 0;


            return View();
        }
    }
}
