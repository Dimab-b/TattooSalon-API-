using MediatR;
using WebApiArchitecture.Common;
using WebApiArchutecture.Application.DTOs;
using WebApiArchutecture.Application.DTOs.TattooDto.TattooReadDto;

namespace WebApiArchutecture.Application.Features.Tattoos.Queries.GetTattoos;

    public record GetTattoosQuery(PaginationParams Params) : IRequest<IEnumerable<TattooReadDto>> , ICachedQuery
    {

    public string CacheKey => CacheKeys.AllTattoos;
    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}