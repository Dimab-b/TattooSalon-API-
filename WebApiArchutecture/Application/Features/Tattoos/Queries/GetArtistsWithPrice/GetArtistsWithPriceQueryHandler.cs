using AutoMapper;
using MediatR;
using WebApiArchutecture.Application.DTOs.ArtistDto;
using WebApiArchutecture.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using WebApiArchutecture.Application.Features.Specifications.TattooQuerySpecification;

namespace WebApiArchutecture.Application.Features.Tattoos.Queries.GetArtistsWithPrice
{
    public class GetArtistsWithPriceQueryHandler : IRequestHandler< GetArtistsWithPriceQuery ,IEnumerable<ArtistReadDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public GetArtistsWithPriceQueryHandler (IMapper mapper , IUnitOfWork uow) { _mapper = mapper; _uow = uow; }

        public async Task<IEnumerable<ArtistReadDto>> Handle(GetArtistsWithPriceQuery Params , CancellationToken token)
        {
            var spec = new GetArtistsWithPriceSpecification(Params.price , Params.skip , Params.take);
            var query = await _uow.Artists.ListAsync(spec, token);
            return _mapper.Map<IEnumerable<ArtistReadDto>>(query);
        }
 


    }
}
