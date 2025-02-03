using Microsoft.AspNetCore.Mvc;
using System.IO;
using Tratament.Web.Core.PrintModule;
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

            MemoryStream stream = new MemoryStream(pdfDocument);

            return new FileStreamResult(stream, "application/pdf") { FileDownloadName = "Recipisa.pdf" };

        }
    }
}
