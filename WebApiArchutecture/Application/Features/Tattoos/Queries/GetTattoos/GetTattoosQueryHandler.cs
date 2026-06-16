using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiArchutecture.Application.DTOs;
using WebApiArchutecture.Application.DTOs.TattooDto.TattooReadDto;
using WebApiArchutecture.Infrastructure.UnitOfWork;

namespace WebApiArchutecture.Application.Features.Tattoos.Queries.GetTattoos
{
    public class GetTattoosQueryHandler : IRequestHandler<GetTattoosQuery , IEnumerable<TattooReadDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public GetTattoosQueryHandler(IMapper mapper, IUnitOfWork uow) {        
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<IEnumerable<TattooReadDto>> Handle(GetTattoosQuery Params , CancellationToken token)
        {

                var res = _uow.Tattoos.GetQueryable().AsNoTracking().OrderByDescending(X => X.Id);

                var pagedData = await res.Skip((Params.Params.PageNumber - 1) * Params.Params.PageSize).Take(Params.Params.PageSize).ToListAsync(token);

                var dto = _mapper.Map<IEnumerable<TattooReadDto>>(pagedData);

                return dto;
            

        }

 
    }
}
