using Tratament.Web.ViewModels.SubmitRequest;

namespace Tratament.Web.Core.PrintModule
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
