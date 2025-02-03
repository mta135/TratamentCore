using QuestPDF.Fluent;
using QuestPDF.Helpers;
using Tratament.Web.ViewModels.SendRequest;

namespace Tratament.Web.Core.PrintModule
{
    public class PdfDocumentProcessor
    {
        public byte[] GeneratePdfDocument(SubmitViewModel submitViewModel)
        {
            byte[] pdfDocument = null;

            pdfDocument = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5.Landscape()); // A5 in Landscape mode
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Times New Roman"));

                    page.Content().Column(column =>
                    {
                        column.Spacing(20);

                        column.Item().AlignCenter().Text("Recipisa").FontSize(20).Bold();

                        column.Item().AlignRight().Column(rightColumn =>
                        {
                            rightColumn.Spacing(10); // Adds space between items

                            rightColumn.Item().Text("Casa Nationala de Asigurari Sociale").FontSize(14).Bold().AlignRight();

                            rightColumn.Item().Text("Data recipisei: 2024/01/23").FontSize(14).AlignCenter();

                        });

                        // Additional text below
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(); // Nume câmp
                                columns.RelativeColumn(); // Valoare câmp
                            });

                            string borderColor = "#dee2e6"; // Culoare hexazecimală

                            void AddRow(string field, string value)
                            {
                                table.Cell().Border((float)0.3).BorderColor(borderColor).Padding(5).Text(field);
                                table.Cell().Border((float)0.3).BorderColor(borderColor).Padding(5).Text(value);
                            }

                            string npp = $"{submitViewModel.Name}/{submitViewModel.Surname}/{submitViewModel.Patronymic}/({submitViewModel.Idnp})";

                            AddRow("Cererea depusă de către:", npp);

                            AddRow("Tipul solicitării:", SubmitRequestHelper.GetCompensationTypeTiket(submitViewModel.TicketTypeId));

                            AddRow("Numărul cererii:", submitViewModel.RequestNumber);

                            AddRow("Data depunerii:", Convert.ToString(submitViewModel.RequestSubmitDate));

                            AddRow("Detalii:", "Cererea a fost acceptată cu succes.");
                        });

                        column.Item().LineHorizontal((float)0.3).LineColor("#dee2e6");

                        column.Item().Text("Nota: Statutul cererii poate fi verificat pe o perioadă de 2 luni").FontSize(12).Italic();

                    });
                });

            }).GeneratePdf();

            return pdfDocument;
        }




    }
}

