using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApiArchutecture.Infrastructure.UnitOfWork;

namespace WebApiArchutecture.Application.Features.Auth.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<LogoutCommandHandler> _logger;

        public LogoutCommandHandler(IUnitOfWork uow , ILogger<LogoutCommandHandler> logger  )
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task Handle(LogoutCommand request , CancellationToken token)
        {
            var user = await _uow.Users.GetQueryable().Where(p => p.RefreshToken == request.refreshtoken).FirstOrDefaultAsync(token);
            _logger.LogDebug("Пошук користувачa за рефреш токеном у базі.");
            if (user != null)
            {
                user.RefreshToken = string.Empty;
                user.TokenExpires = DateTime.MinValue;
                user.IsRevoked = true;
                await _uow.SaveAsync(token);
                _logger.LogInformation("Користувач {Username} успішно вийшов з аккаунту" , user.Username);
              
            }

            else {
                _logger.LogWarning("Спроба виходу з невалідним Refresh Token.");
                throw new Exception(); }
        }
    }
}
