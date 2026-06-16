using Microsoft.EntityFrameworkCore;
using WebApiArchutecture.Domain;

namespace WebApiArchutecture.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<Tattoo> Tattoos { get; set; } = null!;
        public DbSet<SignUpForTattoo> SignUps { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Artist>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<Tattoo>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<SignUpForTattoo>().Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
