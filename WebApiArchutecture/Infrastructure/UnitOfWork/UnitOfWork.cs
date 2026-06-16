using AutoMapper;
using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.Repositories.IArtistRepository;
using WebApiArchutecture.Infrastructure.Repositories.SignUpForTattooRepository;
using WebApiArchutecture.Infrastructure.Repositories.TattooRepository;
using WebApiArchutecture.Infrastructure.Repositories.UserRepository;
using WebApiArchutecture.Infrastructure.Repositories.UsersRepository;

namespace WebApiArchutecture.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ITattooRepository Tattoos { get; private set; }

        public IArtistRepository Artists { get; private set; }

        public ISignUpForTattooRepository SignUps { get; private set; }
        public IUserRepository Users { get; private set; }
        public void Dispose() => _context.Dispose();
        public async Task SaveAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);

        public UnitOfWork(
         AppDbContext context,
         ITattooRepository tattoos,
         IArtistRepository artists,
         ISignUpForTattooRepository signUps,
         IUserRepository users)
        {
            _context = context;
            Tattoos = tattoos;
            Artists = artists;
            SignUps = signUps;
            Users = users;
        }
    }
}
