using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using WebApiArchitecture.Common;

namespace WebApiArchitecture.Infrastructure.Services.Pdf
{
    public class QuestPdfGenerator : IPdfGenerator
    {
        public byte[] GenerateReportPdf(string reportTitle, string content)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Header().Text(reportTitle).FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
                    page.Content().PaddingVertical(10).Text(content);
                    page.Footer().AlignCenter().Text(x => { x.CurrentPageNumber(); });
                });
            }).GeneratePdf();
        }
    }
}
