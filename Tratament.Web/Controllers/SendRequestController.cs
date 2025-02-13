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
using static ServiceReference.BiletePortTypeClient;
using Tratament.Web.Services.Tickets;
using Tratament.Model.Models.EcerereTicketService;
using Tratament.Web.ViewModels.SendRequest.Heleper;



namespace Tratament.Web.Controllers
{
    public class SendRequestController : Controller
    {
        private readonly IRecaptchaService _recaptchaService;

        private readonly IMConnectService _mConnectService;

        private IDNTCaptchaValidatorService _validatorService; 
        private DNTCaptchaOptions _captchoptions;


        private ITicketService _ticketService;

        public SendRequestController(IRecaptchaService recaptchaService, IMConnectService mConnectService, IDNTCaptchaValidatorService validatorService, IOptions<DNTCaptchaOptions> captchaOptions, ITicketService ticketService)
        {    
            _recaptchaService = recaptchaService;
            _mConnectService = mConnectService;

            _validatorService = validatorService;
            _captchoptions = captchaOptions == null ? throw new ArgumentNullException(nameof(captchaOptions)) : captchaOptions.Value;

            _ticketService = ticketService;
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
                TicketInsertModel insertModel = SetTicketInsertData(mconnectPerson, requestViewModel); // acest obiect va trebui transmis in sesiune...

                string cerereId = await _ticketService.InsertTicketToEcerere(insertModel);
    
                SubmitViewModel submitViewModel = SetSubmitedData(mconnectPerson, cerereId, requestViewModel.TicketTypeId);     // nu stiu daca trebui aceasta clasa.

                HttpContext.Session.SetObject("SubmitData", submitViewModel);

                return RedirectToAction("Submited", "SendRequest");

            }
            
            return View();
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

        public async Task<IActionResult> TestMconnect() // aceasta metoda va trebui stearsa
        {

            try
            {
                PersonFilter personFilter = new PersonFilter();
                personFilter.IDNP = "2010500696009";

                ServiceReference.BiletePortTypeClient client = new ServiceReference.BiletePortTypeClient(EndpointConfiguration.SOAP11Endpoint);
                var value = await client.ins_ecerereAsync(1, "2010500696009", "MTA", "MTA", "4598", "Adresa client", "7895321", "@gmail.com", null, "M");

            }
            catch (Exception ex)
            {
                string request = ex.ToString();
            }

            return View();

            //return Content("Mconnect IDNP: " + mconnectPerson.IDNP);

        }


        // cread ca asteasca funtie va trebuie de transferta in alta clasa
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


        public TicketInsertModel SetTicketInsertData(PersonModel persone, SendRequestViewModel sendRequest)
        {
            TicketInsertModel ticketInsertModel = new TicketInsertModel();

            ticketInsertModel.Vpres_rf = Convert.ToInt16(sendRequest.TicketTypeId);
            ticketInsertModel.Vidnp = persone.IDNP;

            ticketInsertModel.Vnume = persone.Name;
            ticketInsertModel.Vprenume = persone.Surname;
            ticketInsertModel.Vcuatm = persone.PersoneAddress.AdministrativeCode;

            ticketInsertModel.Vadresa = SetPersoneAddress(persone.PersoneAddress);
            ticketInsertModel.Vtelefon = sendRequest.Phone;
            ticketInsertModel.Vemail = sendRequest.Email;

            ticketInsertModel.VnascutD = persone.DateOfBirth;
            ticketInsertModel.Vsex = null;

            return ticketInsertModel;
        }

        private string SetPersoneAddress(PersoneAddress adr)
        {
            string address = adr.Country + " " + adr.Region + " " + adr.Locality + " " + adr.Locality + " " + adr.Street + " " + adr.House + " " + adr.Block + " " + adr.Flat;

            return SubmitRequestHelper.NormalizeStringLength(address, 50);
        }

    }
}
