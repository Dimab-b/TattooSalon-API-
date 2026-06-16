using MediatR;
using WebApiArchutecture.Application.DTOs.UserDto;
using WebApiArchutecture.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace WebApiArchutecture.Application.Features.Auth.Commands.Loggin
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand , LoginResponseDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;
        private readonly ILogger<LoginUserCommandHandler> _logger;
        public LoginUserCommandHandler(IUnitOfWork uow , IConfiguration config , ILogger<LoginUserCommandHandler> logger )
        {
            _uow = uow;
            _config = config;
            _logger = logger;
        }

        public async Task<LoginResponseDto> Handle(LoginUserCommand command , CancellationToken token)
        {
            var existingUser = await _uow.Users.GetQueryable()
        .FirstOrDefaultAsync(u => u.Username == command.Username, token);


            if (existingUser != null && BCrypt.Net.BCrypt.Verify(command.Password, existingUser.PasswordHash))
            {
                var accessToken = TokenHelper.CreateToken(existingUser , _config) ;
                _logger.LogInformation("Згенерували Токен . Користувачу - {Username}" , command.Username);
                var refreshToken = TokenHelper.GenerateRefreshToken();

                existingUser.RefreshToken = refreshToken.Token;
                existingUser.TokenCreated = refreshToken.Created;
                existingUser.TokenExpires = refreshToken.Expires;

                await _uow.SaveAsync(token);
                _logger.LogInformation("{Username} - зберегли Токен в базу." , command.Username);

                return new LoginResponseDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };

            }
            else throw new UnauthorizedAccessException("Невірний логін або пароль");
        }


    }


}

