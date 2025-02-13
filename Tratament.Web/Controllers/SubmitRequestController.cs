using Microsoft.AspNetCore.Mvc;
using Tratament.Web.Core.PrintModule;
using Tratament.Web.Core;
using Tratament.Model.Models.Enums;
using Tratament.Web.ViewModels.SubmitRequest;

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

        public IActionResult Error(int error)
        {
            ErrorViewModel errorViewModel = SetErrorType(error);

            return View("~/Views/SubmitRequest/Error.cshtml", errorViewModel);
        }

        public ActionResult PrintPdfDocument()
        {
            SubmitViewModel submitViewModel = HttpContext.Session.GetObject<SubmitViewModel>("SubmitData");

            PrintHelper printHelper = new PrintHelper();

            byte[] pdfDocument = printHelper.PrintPdf(submitViewModel);

            return File(pdfDocument, "application/pdf", "Recipisa.pdf");

        }

        private ErrorViewModel SetErrorType(int error)
        {
            ErrorViewModel errorViewModel = new ErrorViewModel();

            switch(error)
            {
                case (int)ErrorTypeEnum.MconnectError:
                    errorViewModel.ErrorText = "A apărut o eroare la recepționarea datelor prin serviciul Mconnect.";
                    break;

                case (int)ErrorTypeEnum.InsertToCnasError:
                    errorViewModel.ErrorText = "A apărut o eroare la transmiterea datelor la CNAS.";
                    break;
            }

            return errorViewModel;
        }

    }
}
