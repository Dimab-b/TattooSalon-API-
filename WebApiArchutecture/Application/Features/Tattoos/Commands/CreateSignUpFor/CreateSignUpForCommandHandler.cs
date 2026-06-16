using AutoMapper;
using FluentValidation;
using MediatR;
using System.Runtime.CompilerServices;
using WebApiArchutecture.Application.DTOs.SignUpForDto;
using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.UnitOfWork;

namespace WebApiArchutecture.Application.Features.Tattoos.Commands.CreateSignUpFor
{
    public class CreateSignUpForCommandHandler : IRequestHandler<CreateSignUpForCommand , SignUpForTattooReadDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSignUpForCommandHandler> _logger;

        public CreateSignUpForCommandHandler(IUnitOfWork uow, IMapper mapper , ILogger<CreateSignUpForCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;;
            _logger = logger;
        }

        public async Task<SignUpForTattooReadDto> Handle(CreateSignUpForCommand command , CancellationToken token)
        {
            if (command.TimeOfSign < DateTime.UtcNow.AddHours(+24))
            {
                _logger.LogWarning("Спроба створити запис на минуле , або менше ніж 24 години від сеансу");
                throw new Exception("Не можна записатися на минуле, або менше ніж 24 години від сеансу");
            }
            else if (command.Sessions > 15) { _logger.LogWarning("Спроба зробити запис більш ніж на 15 сесій"); throw new Exception("Робимо до 15 сесій"); }



            else
            {
                
                var registration = _mapper.Map<SignUpForTattoo>(command);
                await _uow.SignUps.Create(registration, token);
                await _uow.SaveAsync(token);
                _logger.LogInformation("Успішна реєстрація на запис на {Time}" , command.TimeOfSign);
                return _mapper.Map<SignUpForTattooReadDto>(registration);
            }

        }
    }
}
