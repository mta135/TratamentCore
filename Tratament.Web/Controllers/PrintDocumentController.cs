using Microsoft.AspNetCore.Mvc;
using Tratament.Web.Core;

namespace Tratament.Web.Controllers
{
    public class PrintDocumentController : Controller
    {
        public ActionResult PrintPdfDocument()
        {
            PrintHelper printHelper = new PrintHelper();

            byte[] pdfDocument = printHelper.PrintPdf();

            return File(pdfDocument, "application/pdf", "Recipisa.pdf");
        }
    }
}
