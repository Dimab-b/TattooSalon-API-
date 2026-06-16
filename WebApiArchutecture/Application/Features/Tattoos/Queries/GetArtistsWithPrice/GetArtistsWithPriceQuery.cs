using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebApiArchitecture.Common;
using WebApiArchutecture.Application.DTOs;
using WebApiArchutecture.Application.DTOs.ArtistDto;

namespace WebApiArchutecture.Application.Features.Tattoos.Queries.GetArtistsWithPrice
{
    public record GetArtistsWithPriceQuery(PaginationParams Params , int price) : IRequest<IEnumerable<ArtistReadDto>> 
    {
        public int skip =>  (Params.PageNumber - 1) * Params.PageSize;
        public int take => Params.PageSize;


    }

    
    
}
