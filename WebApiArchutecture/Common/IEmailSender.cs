namespace WebApiArchitecture.Common
{
    public interface IEmailSender
    {
        Task SendAsync(string to, string subject, string body, byte[] attachment, string fileName);
    }
}
