using WebApiArchutecture.Application.DTOs;
using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.Repositories.GenericRepository;

namespace WebApiArchutecture.Infrastructure.Repositories.UsersRepository
{
    public interface IUserRepository : IGenericRepository<User>;
    
    
}
