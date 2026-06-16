using Microsoft.EntityFrameworkCore;
using WebApiArchitecture.Common;
using WebApiArchitecture.Infrastructure.Services.Email;
using WebApiArchitecture.Infrastructure.Services.Pdf;
using WebApiArchutecture.Infrastructure.Repositories.IArtistRepository;
using WebApiArchutecture.Infrastructure.Repositories.SignUpForTattooRepository;
using WebApiArchutecture.Infrastructure.Repositories.TattooRepository;
using WebApiArchutecture.Infrastructure.Repositories.UserRepository;
using WebApiArchutecture.Infrastructure.Repositories.UsersRepository;
using WebApiArchitecture.Infrastructure;
using WebApiArchutecture.Infrastructure.UnitOfWork;


namespace WebApiArchutecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration , IWebHostEnvironment env)
    {
        if (env.IsEnvironment("Testing"))
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("IntegrationTestingDb"));
        }
        else
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        }

        services.AddScoped<IUnitOfWork, WebApiArchutecture.Infrastructure.UnitOfWork.UnitOfWork>();
        services.AddScoped<ITattooRepository, TattooRepository>();
        services.AddScoped<ISignUpForTattooRepository, SignUpForTattooRepository>();
        services.AddScoped<IArtistRepository, ArtistRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IPdfGenerator, QuestPdfGenerator>();
        services.AddScoped<IEmailSender, FluentEmailSender>();

        return services;
    }
}