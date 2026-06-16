using FluentEmail.Core;
using WebApiArchitecture.Common;

namespace WebApiArchitecture.Infrastructure.Services.Email
{
    public class FluentEmailSender : IEmailSender
    {
        private readonly IFluentEmail _fluentEmail;

        public FluentEmailSender(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task SendAsync(string to, string subject, string body, byte[] attachment, string fileName)
        {
            await _fluentEmail
                .To(to)
                .Subject(subject)
                .Body(body)
                .Attach(new FluentEmail.Core.Models.Attachment
                {
                    Data = new MemoryStream(attachment),
                    Filename = fileName,
                    ContentType = "application/pdf"
                })
                .SendAsync();
        }
    }
}
