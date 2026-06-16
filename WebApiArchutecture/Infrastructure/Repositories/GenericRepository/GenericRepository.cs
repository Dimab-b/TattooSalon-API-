using Microsoft.EntityFrameworkCore;
using WebApiArchutecture.Application.Features.Specifications;
using WebApiArchutecture.Domain;

namespace WebApiArchutecture.Infrastructure.Repositories.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        
        public GenericRepository(AppDbContext context ) { _context = context; } 


        public IQueryable<T> GetQueryable()
        {
            return  _context.Set<T>().AsQueryable();
        }

        public async Task<T> GetById(int id, CancellationToken cancellationToken) 
        {
            return await _context.Set<T>().FindAsync(id , cancellationToken);
        }

        public async Task Create(T entity , CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(entity);

        }

        public void Delete(IEnumerable<T> entities )
        {

            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            else { _context.Set<T>().RemoveRange(entities); }
        }

        public void Update(T entity)
        {
            if (entity != null)
            {
                _context.Set<T>().Update(entity);
            }
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec , CancellationToken cancellationToken )
        {
            return await SpecificationEvaluator<T>.GetQuery(_context.Set<T>() , spec).AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}

