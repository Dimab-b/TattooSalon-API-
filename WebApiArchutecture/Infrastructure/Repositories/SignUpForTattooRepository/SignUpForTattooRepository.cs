using WebApiArchutecture.Infrastructure.Repositories.GenericRepository;
using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.Repositories.SignUpForTattooRepository;
namespace WebApiArchutecture.Infrastructure.Repositories.SignUpForTattooRepository;

public class SignUpForTattooRepository : GenericRepository<Domain.SignUpForTattoo> , ISignUpForTattooRepository
{
    public SignUpForTattooRepository(AppDbContext context ) : base(context )
    {
    }
}
