using MediatR;
using WebApiArchutecture;
using WebApiArchutecture.Application;
using WebApiArchutecture.Application.DTOs;
using WebApiArchutecture.Application.DTOs.UserDto;
using WebApiArchutecture.Application.Features;
using WebApiArchutecture.Application.Features.AdminPanel;
using WebApiArchutecture.Application.Features.AdminPanel.Queries.GetAllUsers;
using WebApiArchutecture.Application.Features.AdminPanel.Queries.GetAllUsers;
using WebApiArchutecture.Domain;

namespace WebApiArchutecture.Application.Features.AdminPanel.Queries.GetAllUsers
{
    public record GetArtistWithPriceQuery
    (PaginationParams Params) : IRequest<IEnumerable<UserReadDto>>;
    
}
