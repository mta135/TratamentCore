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
                    page.Size(PageSizes.A4);

                    page.Margin(50);
                    page.Content().Column(column =>
                    {
                        column.Item().Text("Hello, QuestPDF!").FontSize(20).Bold();
                        column.Item().Text("This is a simple PDF document created using QuestPDF.").FontSize(14);
                    });
                });

            }).GeneratePdf();


            return pdfDocument;
        }

    }
}
