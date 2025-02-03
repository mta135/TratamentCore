using DNTCaptcha.Core;
using Tratament.Web.ApiViewModels.ExternalServices;
using Tratament.Web.Services.MConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Tratament.Web.LoggerSetup;
using Tratament.Web.Recaptcha.Interface;
using Tratament.Web.ViewModels.SendRequest;
using Tratament.Web.Services.MConnect.Models.Person;

namespace Tratament.Web.Controllers
{
    public class SendRequestController : Controller
    {
        private readonly IRecaptchaService _recaptchaService;

        private readonly IMConnectService _mConnectService;

        private IDNTCaptchaValidatorService _validatorService; 
        private DNTCaptchaOptions _captchoptions;

        public SendRequestController(IRecaptchaService recaptchaService, IMConnectService mConnectService, IDNTCaptchaValidatorService validatorService, IOptions<DNTCaptchaOptions> captchaOptions)
        {    
            _recaptchaService = recaptchaService;
            _mConnectService = mConnectService;

            _validatorService = validatorService;
            _captchoptions = captchaOptions == null ? throw new ArgumentNullException(nameof(captchaOptions)) : captchaOptions.Value;
        }

        [HttpGet]
        public IActionResult Send()
        {
            WriteLog.Common.Debug("SendRequestController/Send");
            
            SendRequestViewModel requestViewModel = new();

            ViewBag.TicketTypes = GetTicketTypes();

            return View(requestViewModel);
        }

        [HttpPost]
        public IActionResult Send(SendRequestViewModel requestViewModel)
        {
            ViewBag.TicketTypes = GetTicketTypes();


            if(!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
            {
                ViewBag.IsVerified = true;
                ModelState.AddModelError(_captchoptions.CaptchaComponent.CaptchaInputName, "Introduce-ți codul de siguranță MTA"); 

                return View("Send");
            }


            //#region google reCaptha

            //string captchaResponse = Request.Form["g-recaptcha-response"].ToString();

            //if (!await _recaptchaService.VerifyRecaptchaAsync(captchaResponse))
            //{
            //    ViewBag.IsVerified = true;
            //    return View("Send");
            //}

            //#endregion

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
            personFilter.IDNP = "2002089016965";

            PersonAPIModel personAPI = await _mConnectService.GetPerson(personFilter);
            var a = 0;


            return View();
        }
    }
}
