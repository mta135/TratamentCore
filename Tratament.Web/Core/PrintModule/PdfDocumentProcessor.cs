﻿using QuestPDF.Fluent;
using QuestPDF.Helpers;
using Tratament.Web.ViewModels.SendRequest.Heleper;
using Tratament.Web.ViewModels.SubmitRequest;

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

                        column.Item().AlignCenter().Text("Recipisă").FontSize(20).Bold();

                        column.Item().AlignRight().Column(rightColumn =>
                        {
                            rightColumn.Spacing(10); // Adds space between items

                            rightColumn.Item().Text("Casa Naționala de Asigurări Sociale").FontSize(14).Bold().AlignRight();

                            rightColumn.Item().Text("Data recipisei: " + submitViewModel.RequestSubmitDate.Value.ToString("dd/MM/yyyy")).FontSize(14).AlignCenter();

                        });

                        // Additional text below
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(); // Nume câmp
                                columns.RelativeColumn(); // Valoare câmp
                            });

                            string borderColor = "#dee2e6"; 

                            void AddRow(string field, string value)
                            {
                                table.Cell().Border((float)0.3).BorderColor(borderColor).Padding(5).Text(field);
                                table.Cell().Border((float)0.3).BorderColor(borderColor).Padding(5).Text(value);
                            }

                            AddRow("Cererea depusă de către:", submitViewModel.Idnp);

                            AddRow("Tipul solicitării:", SubmitRequestHelper.GetCompensationTypeTiket(submitViewModel.TicketTypeId));

                            AddRow("Numărul cererii:", submitViewModel.RequestNumber);

                            AddRow("Data depunerii:", submitViewModel.RequestSubmitDate.Value.ToString("dd/MM/yyyy"));

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

