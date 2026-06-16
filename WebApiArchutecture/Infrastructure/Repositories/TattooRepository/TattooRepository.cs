using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.Repositories.GenericRepository;

namespace WebApiArchutecture.Infrastructure.Repositories.TattooRepository
{
    public class TattooRepository : GenericRepository<Tattoo>, ITattooRepository
    {
        public TattooRepository(AppDbContext context ) : base(context  )
        {
        }
    public IQueryable<Tattoo> GetBlackTattoos()
        {
            var query = GetQueryable().Where(p => p.Color.Equals(Color.black));
            return query;
        }
    
    }
}
