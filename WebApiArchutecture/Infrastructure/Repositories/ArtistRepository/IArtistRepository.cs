using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.Repositories.GenericRepository;

namespace WebApiArchutecture.Infrastructure.Repositories.IArtistRepository
{
    public interface IArtistRepository : IGenericRepository<Artist>
    {
    }
}
