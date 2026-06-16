using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using WebApiArchitecture.Common;
using WebApiArchutecture.Application.DTOs.ArtistDto;
using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.UnitOfWork;
namespace WebApiArchutecture.Application.Features.Tattoos.Commands.CreateArtist
{
    public class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand , ArtistReadDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateArtistCommandHandler> _logger;

        public CreateArtistCommandHandler(IUnitOfWork uow, IMapper mapper  ,  ILogger<CreateArtistCommandHandler> logger ) { _uow = uow; _mapper = mapper; _logger = logger; }


        public async Task<ArtistReadDto> Handle(CreateArtistCommand command , CancellationToken token)
        {

           if (command == null)
            {
                _logger.LogWarning("Спроба створити Майстра з не до кінця/незаповненими даними");
                throw new ArgumentNullException(nameof(command));
            }
            var res = _mapper.Map<Artist>(command);
            await _uow.Artists.Create(res, token);
            await _uow.SaveAsync(token);
            _logger.LogInformation("Майстер: {Username} - Успішно створений" , command.Name);

            return _mapper.Map<ArtistReadDto>(res);
        }
    }
}
