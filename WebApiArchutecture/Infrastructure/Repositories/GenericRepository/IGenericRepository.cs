using WebApiArchutecture.Application.Features.Specifications;
using WebApiArchutecture.Domain;

namespace WebApiArchutecture.Infrastructure.Repositories.GenericRepository
{
    public interface IGenericRepository <T> where T : class
    {
        
        Task<T> GetById(int id , CancellationToken cancellationToken);
        void Update(T entity);
        void Delete(IEnumerable<T> entities ) ;
        Task Create(T entity , CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
        IQueryable<T> GetQueryable();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken);


    }
}
