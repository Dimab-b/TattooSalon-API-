using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiArchutecture.Application.DTOs;
using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.Repositories.GenericRepository;
using WebApiArchutecture.Infrastructure.Repositories.UsersRepository;
using WebApiArchutecture.Infrastructure.UnitOfWork;

namespace WebApiArchutecture.Infrastructure.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        
        private readonly AppDbContext _context;
      
        public UserRepository(AppDbContext context  ) : base(context)
        {
            _context = context;
          
        }

        //public async Task<IEnumerable<User>> GetAllUsers(PaginationParams @params)
        //{
        //    var Users = _context.users.GetQueryable().AsNoTracking().OrderBy(x=> x.Id);
        //    var pagedData = await Users.Skip((@params.PageNumber - 1) * @params.PageSize).Take(@params.PageSize).ToListAsync();
        //    return _mapper.Map<IEnumerable<User>>(pagedData);
        //}
    }
}
