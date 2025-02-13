using Microsoft.AspNetCore.Mvc;
using Tratament.Web.Core.PrintModule;
using Tratament.Web.Core;
using Tratament.Web.ViewModels.SendRequest;
using Tratament.Web.ViewModels.Error;

namespace Tratament.Web.Controllers
{
    public class SubmitRequestController : Controller
    {
        [HttpGet]
        public IActionResult Submited()
        {
            SubmitViewModel submitViewModel = new SubmitViewModel();

            submitViewModel = HttpContext.Session.GetObject<SubmitViewModel>("SubmitData");

            if (submitViewModel == null)
                submitViewModel = new SubmitViewModel();

            return View(submitViewModel);
        }

        public IActionResult Error(int errorType)
        {
            ErrorViewModel errorViewModel = SetErrorType(errorType);

            return View("~/Views/SubmitRequest/Error.cshtml", errorViewModel);
        }

        public ActionResult PrintPdfDocument()
        {
            SubmitViewModel submitViewModel = HttpContext.Session.GetObject<SubmitViewModel>("SubmitData");

            PrintHelper printHelper = new PrintHelper();

            byte[] pdfDocument = printHelper.PrintPdf(submitViewModel);

            return File(pdfDocument, "application/pdf", "Recipisa.pdf");

        }

        private ErrorViewModel SetErrorType(int errorType)
        {
            ErrorViewModel errorViewModel = new ErrorViewModel();

            switch(errorType)
            {
                case 1:
                    errorViewModel.ErrorText = "A aparut o eroare la receptionarea datelor din Serviciul Mconect";
                    break;

                case 2:
                    errorViewModel.ErrorText = "A aparut o eroare a transmiterea datelor la CNAS.";
                    break;
            }

            return errorViewModel;
        }

    }
}
