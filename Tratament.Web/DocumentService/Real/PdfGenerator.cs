using QuestPDF.Fluent;
using QuestPDF.Helpers;
using Tratament.Web.DocumentService.IDocumentService;

namespace Tratament.Web.DocumentService.Workers
{
    public class PdfGenerator : IPdfGenerator
    {

        public byte[] GeneratePdfDocument()
        {
            byte[] pdfDocument = null;

            pdfDocument = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5.Landscape());
                    page.Margin(30);
                    page.Content().Column(column =>
                    {
                        column.Spacing(5); // Adds space between lines

                        column.Item().Text("First line of text").FontSize(12);
                        column.Item().Text("Second line of text").FontSize(12);
                        column.Item().Text("Third line of text").FontSize(12);
                    });
                });

            }).GeneratePdf();


            return pdfDocument;
        }

    }
}
