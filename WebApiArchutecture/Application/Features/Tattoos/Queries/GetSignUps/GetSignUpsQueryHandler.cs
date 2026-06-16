using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiArchitecture.Application.Features.Specifications.SignUpsQuerySpecification;
using WebApiArchutecture.Application.DTOs.SignUpForDto;
using WebApiArchutecture.Infrastructure.UnitOfWork;

namespace WebApiArchutecture.Application.Features.Tattoos.Queries.GetSignUps
{
    public class GetSignUpsQueryHandler : IRequestHandler<GetSignUpsQuery , IEnumerable<SignUpForTattooReadDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public GetSignUpsQueryHandler (IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<IEnumerable<SignUpForTattooReadDto>> Handle(GetSignUpsQuery query , CancellationToken token)
        {
            
            var spec = new GetSignUpsQuerySpecification(query.skip , query.take);
            var res = await _uow.SignUps.ListAsync(spec , token);
            return _mapper.Map<IEnumerable<SignUpForTattooReadDto>>(res);
        }
    }
}
