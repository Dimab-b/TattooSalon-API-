using MediatR;
using WebApiArchutecture.Application.DTOs;
using WebApiArchutecture.Application.DTOs.SignUpForDto;

namespace WebApiArchutecture.Application.Features.Tattoos.Queries.GetSignUps
{
    public record GetSignUpsQuery(PaginationParams Params) : IRequest<IEnumerable<SignUpForTattooReadDto>>
    {
        public int skip => (Params.PageNumber - 1) * Params.PageSize;
        public int take => Params.PageSize;
    };
    
    
}
