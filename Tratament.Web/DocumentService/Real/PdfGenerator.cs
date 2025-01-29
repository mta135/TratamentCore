using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Data.Common;
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
                    page.Size(PageSizes.A5.Landscape()); // A5 in Landscape mode
                    page.Margin(30);

                    page.Content().Column(column =>
                    {
                        column.Spacing(20); // Space between items

                        // Centered text "Recipisa"
                        column.Item().AlignCenter().Text("Recipisa")
                            .FontSize(20)
                            .Bold();

                        // Right-aligned container for "Casa Nationala de Asigurari Sociale" and "Data recipisei"
                        column.Item().AlignRight().Column(rightColumn =>
                        {
                            rightColumn.Spacing(10); // Adds space between items

                            rightColumn.Item().Text("Casa Nationala de Asigurari Sociale")
                                .FontSize(14)
                                .Bold()
                                .AlignRight(); // Ensures it stays on the right

                            rightColumn.Item().Text("Data recipisei: 2024/01/23")
                                .FontSize(14)
                                .AlignCenter(); // Centers it below the first text
                        });

                        // Additional text below
                        column.Item().Column(innerColumn =>
                        {
                            innerColumn.Spacing(10);

                            innerColumn.Item().Text("Cererea depusă de către IDNP").FontSize(14);
                            innerColumn.Item().Text("Tipul solicitării").FontSize(14);
                            innerColumn.Item().Text("Numărul cererii").FontSize(14);
                            innerColumn.Item().Text("Data depunerii").FontSize(14);


                            innerColumn.Item().Text("Detalii: ").FontSize(14);

                            innerColumn.Item().LineHorizontal(1);

                            // Text below the horizontal line
                            innerColumn.Item().Text("Nota: Statului cererii poate fi verificat pe perioada de 2 luni")
                                .FontSize(12).Italic();

                        });
                    });
                });

            }).GeneratePdf();


            return pdfDocument;
        }

    }
}
