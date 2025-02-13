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
using Tratament.Model.Models.Enums;
using Tratament.Web.ViewModels.SubmitRequest;



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
                ModelState.AddModelError(_captchoptions.CaptchaComponent.CaptchaInputName, "Codul de siguranță a fost introdus greșit"); 
                return View("Send");
            }

            #region MConnect

            PersonFilter personFilter = new() { IDNP = requestViewModel.Idnp };
            PersonModel mconnectPerson = await _mConnectService.GetPerson(personFilter);

            if (string.IsNullOrWhiteSpace(mconnectPerson.IDNP))
            {
                return RedirectToAction("Error", "SubmitRequest", new { error = (int)ErrorTypeEnum.MconnectError });
            }

            #endregion

            #region CNAS

            string cerereId = await _ticketService.InsertTicketToEcerere(SetTicketInsertData(mconnectPerson, requestViewModel));

            if (string.IsNullOrWhiteSpace(cerereId))
            {
                return RedirectToAction("Error", "SubmitRequest", new { error = (int)ErrorTypeEnum.InsertToCnasError });
            }

            #endregion

            SubmitViewModel submitModel = SetSubmitedData(mconnectPerson, cerereId, requestViewModel.TicketTypeId);
            HttpContext.Session.SetObject("SubmitData", submitModel);

            return RedirectToAction("Submited", "SubmitRequest");
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

            return adr.Country;  //SubmitRequestHelper.NormalizeStringLength(address, 50);
        }

        private DateTime? BirthDateFormat(DateTime? birthDate)
        {
            if(birthDate != null)
            {
                string temp = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime enteredDate = DateTime.Parse(temp);

                return enteredDate;
            }

            return null;
        }

        public async Task<IActionResult> TestMconnect() // aceasta metoda va trebui stearsa
        {

            try
            {
                PersonFilter personFilter = new PersonFilter();
                personFilter.IDNP = "2010500696009";

                ServiceReference.BiletePortTypeClient client = new ServiceReference.BiletePortTypeClient(EndpointConfiguration.SOAP11Endpoint);
                var value = await client.ins_ecerereAsync(1, "2010500696009", "MTA", "MTA", "4598", "Adresa client", "7895321", "@gmail.com", DateTime.Now, null);

            }
            catch (Exception ex)
            {
                string request = ex.ToString();
            }

            return View();

            //return Content("Mconnect IDNP: " + mconnectPerson.IDNP);

        }

    }
}
