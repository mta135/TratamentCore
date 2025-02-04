using Microsoft.AspNetCore.Mvc;
using Tratament.Web.Core;
using Tratament.Web.Core.PrintModule;
using Tratament.Web.ViewModels.SendRequest;

namespace Tratament.Web.Controllers
{
    public class PrintDocumentController : Controller
    {
        public ActionResult PrintPdfDocument()
        {
            SubmitViewModel submitViewModel = HttpContext.Session.GetObject<SubmitViewModel>("SubmitData");

            PrintHelper printHelper = new PrintHelper();

            byte[] pdfDocument = printHelper.PrintPdf(submitViewModel);

            MemoryStream stream = new MemoryStream(pdfDocument);

            return new FileStreamResult(stream, "application/pdf") { FileDownloadName = "Recipisa.pdf" };

        }
    }
}
