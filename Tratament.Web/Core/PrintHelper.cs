using Tratament.Web.ViewModels.SendRequest;

namespace Tratament.Web.Core
{
    public class PrintHelper
    {
        public byte[] PrintPdf(SubmitViewModel submitViewModel)
        {
            PdfDocumentProcessor pdfDocumentProcessor = new PdfDocumentProcessor();

            byte[] generatedPdf = pdfDocumentProcessor.GeneratePdfDocument(submitViewModel);

            return generatedPdf;
        }
    }
}
