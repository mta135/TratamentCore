using Microsoft.AspNetCore.Mvc;
using Tratament.Web.DocumentService.IDocumentService;

namespace Tratament.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPdfGenerator _pdfGenerator;

        public HomeController(IPdfGenerator pdfGenerator)
        {
            _pdfGenerator = pdfGenerator;
        }

        public IActionResult GeneratePDF()
        {
            byte[] pdf =  _pdfGenerator.GeneratePdfDocument();

            FileContentResult fileContentResult = new(pdf, "application/pdf")
            {
                FileDownloadName = "Recipsia.pdf"
            };

            return fileContentResult;
        }

    }
}
