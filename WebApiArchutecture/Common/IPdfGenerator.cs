namespace WebApiArchitecture.Common
{
    public interface IPdfGenerator
    {
        byte[] GenerateReportPdf(string reportTitle, string content);
    }
}
