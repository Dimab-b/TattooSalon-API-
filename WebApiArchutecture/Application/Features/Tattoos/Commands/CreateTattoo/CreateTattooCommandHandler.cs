using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Runtime.CompilerServices;
using WebApiArchitecture.Common;
using WebApiArchutecture.Application.DTOs.TattooDto;
using WebApiArchutecture.Application.DTOs.TattooDto.TattooReadDto;
using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.UnitOfWork;

namespace WebApiArchutecture.Application.Features.Tattoos.Commands.CreateTattoo
{
    public class CreateTattooCommandHandler : IRequestHandler<CreateTattooCommand , TattooReadDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        private readonly ILogger<CreateTattooCommandHandler> _logger;

        public CreateTattooCommandHandler(IUnitOfWork uow, IMapper mapper, IDistributedCache cache, ILogger<CreateTattooCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _cache = cache;
            _logger = logger;
        }

        public async Task<TattooReadDto> Handle(CreateTattooCommand command , CancellationToken token)
        {
            if (command == null)
            {
                _logger.LogWarning("Спроба створення тату з неповними/пустими даними.");
                throw new ArgumentNullException(nameof(command));
            }
            var res = _mapper.Map<Tattoo>(command);
            await _uow.Tattoos.Create(res, token);
            await _uow.SaveAsync(token);
            _logger.LogInformation("Тату успішно створене {id} " , res.Id);
            await _cache.RemoveAsync(CacheKeys.AllTattoos , token);
            return _mapper.Map<TattooReadDto>(res);
        }
    }
}

