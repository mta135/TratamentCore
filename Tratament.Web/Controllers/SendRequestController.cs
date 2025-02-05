using DNTCaptcha.Core;
using Tratament.Web.Services.MConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Tratament.Web.LoggerSetup;
using Tratament.Web.Recaptcha.Interface;
using Tratament.Web.ViewModels.SendRequest;
using Tratament.Web.Services.MConnect.Models.Person;
using Tratament.Model.Models.ExternalServices;
using Tratament.Web.Core;
using Tratament.Model.Models.Enums;

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
        public async Task<IActionResult> Send(SendRequestViewModel requestViewModel)
        {
            ViewBag.TicketTypes = GetTicketTypes();


            if(!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
            {
                ViewBag.IsVerified = true;
                ModelState.AddModelError(_captchoptions.CaptchaComponent.CaptchaInputName, "Codul de siguranță a fost introdus greșit"); 

                return View("Send");
            }

            PersonFilter personFilter = new();

            personFilter.IDNP = requestViewModel.Idnp;

            PersonModel mconnectPerson = await _mConnectService.GetPerson(personFilter);

            if (mconnectPerson != null)
            {
                SubmitViewModel submitViewModel = SetSubmitedData(mconnectPerson, "62", requestViewModel.TicketTypeId);
                HttpContext.Session.SetObject("SubmitData", submitViewModel);

                return RedirectToAction("Submited", "SendRequest");

            }
            
            return null;
        }

        [HttpGet]
        public IActionResult Submited()
        {
            SubmitViewModel submitViewModel = new SubmitViewModel();

            submitViewModel = HttpContext.Session.GetObject<SubmitViewModel>("SubmitData");

            if (submitViewModel == null)
                submitViewModel = new SubmitViewModel();


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
            personFilter.IDNP = "2010500696009";

            PersonModel mconnectPerson = await _mConnectService.GetPerson(personFilter);

            //if(mconnectPerson != null)
            //{
            //    SubmitViewModel submitViewModel = SetSubmitedData(mconnectPerson, "62", "1");

            //    HttpContext.Session.SetObject("SubmitData", submitViewModel);

            //    return RedirectToAction("Submited", "SendRequest");
            //}

            return Content("Mconnect IDNP: " + mconnectPerson.IDNP);

        }


        public SubmitViewModel SetSubmitedData(PersonModel mconnectPerson, string requestNumber, string ticketTypeId)
        {
            SubmitViewModel submitViewModel = new SubmitViewModel();

            submitViewModel.Idnp = mconnectPerson.IDNP;

            submitViewModel.Name = mconnectPerson.Name;
            submitViewModel.Surname = mconnectPerson.Surname;
            submitViewModel.Patronymic = mconnectPerson.Patronymic;

            submitViewModel.TicketTypeId = ticketTypeId;

            submitViewModel.RequestNumber = requestNumber;

            submitViewModel.RequestSubmitDate = DateTime.Now;

            return submitViewModel;
        }

     }
}
