using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.Repositories.GenericRepository;

namespace WebApiArchutecture.Infrastructure.Repositories.IArtistRepository
{
    public class ArtistRepository : GenericRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(AppDbContext context ) : base(context )
        {
        }
    }
}
