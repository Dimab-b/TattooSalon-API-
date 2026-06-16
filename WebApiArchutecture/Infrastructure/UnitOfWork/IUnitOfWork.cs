using WebApiArchutecture.Infrastructure.Repositories.IArtistRepository;
using WebApiArchutecture.Infrastructure.Repositories.SignUpForTattooRepository;
using WebApiArchutecture.Infrastructure.Repositories.TattooRepository;
using WebApiArchutecture.Infrastructure.Repositories.UsersRepository;

namespace WebApiArchutecture.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public ITattooRepository Tattoos { get; }
        public IArtistRepository Artists {  get; }
        public ISignUpForTattooRepository SignUps { get; }
        public IUserRepository Users { get; }
        public Task SaveAsync(CancellationToken token);
    }
}
