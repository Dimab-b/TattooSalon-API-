using AutoMapper;
using BCrypt.Net;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiArchutecture;
using WebApiArchutecture.Application;
using WebApiArchutecture.Application.DTOs.UserDto;
using WebApiArchutecture.Application.Features;
using WebApiArchutecture.Application.Features.Auth;
using WebApiArchutecture.Application.Features.Auth.Commands;
using WebApiArchutecture.Application.Features.Auth.Commands.Register;
using WebApiArchutecture.Application.Validators;
using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.UnitOfWork;

namespace WebApiArchutecture.Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand , UserReadDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterUserCommandHandler> _logger;
        public RegisterUserCommandHandler (IUnitOfWork uow, IMapper mapper, ILogger<RegisterUserCommandHandler> logger  )
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserReadDto> Handle(RegisterUserCommand command , CancellationToken cancellationToken)
        {


            if (command == null) 
            {
                _logger.LogWarning("Спроба реєстрації з невалідними даними");
               throw new ArgumentNullException(nameof(command));
            }
            


            if (command.Password != command.ConfirmPassword)
            {
                _logger.LogWarning("Спроба реєстрації при неспівпадінні паролей");
                throw new Exception("Пароль повинен співпадати");
            }
                


            var existingUser = await _uow.Users.GetQueryable()
                .FirstOrDefaultAsync(u => u.Username == command.Username);
            if (existingUser != null)
            {
                _logger.LogWarning("Спроба реєстрації на ім'я існуючого користувача");
                throw new Exception("Користувач з таким ім'ям вже існує");
            }


            var user = new User
            {
                Username = command.Username,
                Telegram_Tag = command.Telegram_Tag,
                Role = "User",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password),
                Email = command.Email
            };

          

            await _uow.Users.Create(user , cancellationToken);
            await _uow.SaveAsync(cancellationToken);
            _logger.LogInformation("Зареєстрували користувача: {Username}" , user.Username);
            var res = _mapper.Map<UserReadDto>(user);
            return res ;
        }
    }   
}
