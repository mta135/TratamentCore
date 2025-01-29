using Microsoft.AspNetCore.Mvc;
using Tratament.Web.Core;
using Tratament.Web.ViewModels.SendRequest;

namespace Tratament.Web.Controllers
{
    public class PrintDocumentController : Controller
    {
        public ActionResult PrintPdfDocument()
        {
            SubmitViewModel submitViewModel = new SubmitViewModel();
                        
            PrintHelper printHelper = new PrintHelper();

            byte[] pdfDocument = printHelper.PrintPdf(submitViewModel);

            return File(pdfDocument, "application/pdf", "Recipisa.pdf");
        }
    }
}
