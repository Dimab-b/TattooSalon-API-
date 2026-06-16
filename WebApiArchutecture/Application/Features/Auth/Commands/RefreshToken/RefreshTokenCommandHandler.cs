using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiArchutecture.Application.DTOs.UserDto;
using WebApiArchutecture.Infrastructure.UnitOfWork;

namespace WebApiArchutecture.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResponseDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;
        private readonly ILogger<RefreshTokenCommandHandler> _logger;

        public RefreshTokenCommandHandler(IUnitOfWork uow, IConfiguration config, ILogger<RefreshTokenCommandHandler> logger)
        {
            _uow = uow;
            _config = config;
            _logger = logger;
        }

        public async Task<LoginResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            
         
                var user = await _uow.Users.GetQueryable()
                    .FirstOrDefaultAsync(u => u.RefreshToken == request.refreshToken, cancellationToken);
            


            if (user == null)
            {
                _logger.LogWarning("Спроба поновлення з невалідним або неіснуючим Refresh Token.");
                throw new Exception("Invalid Refresh Token");
            }

            if (user.TokenExpires < DateTime.UtcNow)
            {
                _logger.LogWarning("Спроба використання простроченого токена користувачем {Username}", user.Username);
                throw new Exception("Token expired");
            }

            if (user.IsRevoked)
            {
                _logger.LogWarning("Спроба використання відкликаного (revoked) токена користувачем {Username}", user.Username);
                throw new Exception("Token is revoked");
            }

            _logger.LogDebug("Пошук користувача: {Username} за рефреш токеном.", user.Username);


            var newAccessToken = TokenHelper.CreateToken(user, _config);
                var newRefreshToken = TokenHelper.GenerateRefreshToken();


                user.RefreshToken = newRefreshToken.Token;
                user.TokenCreated = newRefreshToken.Created;
                user.TokenExpires = newRefreshToken.Expires;

                await _uow.SaveAsync(cancellationToken);
            _logger.LogInformation("Поновили рефреш токен користувачу - {Username}" , user.Username);

                return new LoginResponseDto
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                }; ;
            
        }
    }
}







        

