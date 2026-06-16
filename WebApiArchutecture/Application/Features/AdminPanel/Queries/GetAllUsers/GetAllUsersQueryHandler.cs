using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiArchutecture;
using WebApiArchutecture.Application;
using WebApiArchutecture.Application.DTOs;
using WebApiArchutecture.Application.DTOs.UserDto;
using WebApiArchutecture.Application.Features;
using WebApiArchutecture.Application.Features.AdminPanel;
using WebApiArchutecture.Application.Features.AdminPanel.Queries.GetAllUsers;
using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.UnitOfWork;

namespace WebApiArchutecture.Application.Features.AdminPanel.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetArtistWithPriceQuery, IEnumerable<UserReadDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserReadDto>> Handle(GetArtistWithPriceQuery request, CancellationToken cancellationToken)
    {
        
        var usersQuery = _uow.Users.GetQueryable().AsNoTracking().OrderBy(x => x.Id);
        

        var pagedData = await usersQuery
            .Skip((request.Params.PageNumber - 1) * request.Params.PageSize)
            .Take(request.Params.PageSize)
            .ToListAsync(cancellationToken);

        var res = _mapper.Map<IEnumerable<UserReadDto>>(pagedData);



        return res;
    }
}