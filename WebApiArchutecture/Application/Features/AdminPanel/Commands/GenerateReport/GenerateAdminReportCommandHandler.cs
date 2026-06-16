using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebApiArchitecture.Common;
using WebApiArchutecture.Infrastructure;
using WebApiArchutecture.Infrastructure.UnitOfWork;

namespace WebApiArchitecture.Application.Features.AdminPanel.Commands.GenerateReport
{
    public class GenerateAdminReportCommandHandler : IRequestHandler<GenerateAdminReportCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IEmailSender _emailsender;
        private readonly AppDbContext _context;
        private readonly IPdfGenerator _pdfgenerator;
        private readonly ILogger<GenerateAdminReportCommandHandler> _logger;

        public GenerateAdminReportCommandHandler(IUnitOfWork uow, IEmailSender emailsender, AppDbContext context, IPdfGenerator pdfgenerator , ILogger<GenerateAdminReportCommandHandler> logger    )
        {
            _uow = uow;
            _emailsender = emailsender;
            _context = context;
            _pdfgenerator = pdfgenerator;
            _logger = logger;
        }

        public async Task Handle(GenerateAdminReportCommand command , CancellationToken token)
        {
            var tattoosCount = _context.Tattoos.Count();
            var signUpsCount = _context.SignUps.Count();
            var usersCount = _context.Users.Count();
            var artistsCount = _context.Artists.Count();


            var signUps = await _uow.SignUps.GetAll(token);
            var admin = _context.Users.FirstOrDefault(x => x.Role == "Admin");

            var sb = new StringBuilder();

            sb.AppendLine($"Звіт по тату-салону:");
            sb.AppendLine($"----------------------");
            sb.AppendLine($"Всього записів: {signUpsCount}");
            sb.AppendLine($"Всього майстрів: {artistsCount}");
            sb.AppendLine($"Всього користувачів: {usersCount}");
            sb.AppendLine($"Всього тату в базі: {tattoosCount}");
            sb.AppendLine("Всі актуальні записи нашого салону:");
            sb.AppendLine($"{"Номер запису",-20} | {"Дата",-15} | {"Кількість Сесій",-20}");
            foreach (var app in signUps)
            {
                sb.AppendLine($"{app.Id,-20} | {app.TimeOfSign:yyyy-MM-dd} | {app.Sessions,-20}");
            }
            sb.AppendLine($"----------------------");
            sb.AppendLine($"Дата генерації: {DateTime.UtcNow}");
            string content = sb.ToString();
            _logger.LogInformation("Звіт успішно згенерований");
            var pdfBytes = _pdfgenerator.GenerateReportPdf("Звіт Тату-Салону" , content);


            await _emailsender.SendAsync(
            admin.Email,
            "Щотижневий звіт роботи",
            "Вітаю, вкладено звіт по статистиці сайту.",
            pdfBytes,
            "Report.pdf"
        );
            _logger.LogInformation("Задача по відправленню звіта на пошту створена.");
        }
    }   
}
