namespace Tratament.Web.Core
{
    public class PrintHelper
    {
        public byte[] PrintPdf()
        {
            PdfDocumentProcessor pdfDocumentProcessor = new PdfDocumentProcessor();

            byte[] generatedPdf = pdfDocumentProcessor.GeneratePdfDocument();

            return generatedPdf;
        }
    }
}
