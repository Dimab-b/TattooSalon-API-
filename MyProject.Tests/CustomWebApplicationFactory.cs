using Castle.Components.DictionaryAdapter;
using Hangfire;
using Hangfire.Common;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using WebApiArchutecture.Infrastructure;

namespace MyProject.Tests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
       
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                services.AddAuthentication("TestScheme").AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });

                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDistributedCache));
                if (descriptor != null) services.Remove(descriptor);

                var serverDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(BackgroundJobServer));
                if (serverDescriptor != null) services.Remove(serverDescriptor);

                


                services.AddHangfire(config => config.UseMemoryStorage());

                var sp = services.BuildServiceProvider();
                services.AddDistributedMemoryCache();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AppDbContext>();


                    //db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();


                    builder.ConfigureAppConfiguration((context, config) =>
                    {
                        config.AddInMemoryCollection(new Dictionary<string, string?>
                        {
                            {"Hangfire:JobName", "cleanup-signups-" + Guid.NewGuid().ToString()}
                        });

                        config.AddInMemoryCollection(new Dictionary<string, string?>
                        {
                             {"EmailSettings:SmtpServer", "localhost"},
                             {"EmailSettings:Port", "587"},
                             {"EmailSettings:Username", "test"},
                             {"EmailSettings:Password", "test"},
                             {"EmailSettings:From", "test@test.com"}
                         });
                    });
                }
            }!);
      
        }
    }
}



