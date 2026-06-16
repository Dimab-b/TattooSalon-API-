using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.Repositories.GenericRepository;

namespace WebApiArchutecture.Infrastructure.Repositories.TattooRepository
{
    public interface ITattooRepository : IGenericRepository<Tattoo>
    {
        public IQueryable<Tattoo> GetBlackTattoos();
    }
}
