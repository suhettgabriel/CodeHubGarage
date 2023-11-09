using CodeHubGarage.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodeHubGarage.API.Data
{
    public class GarageDbContext : DbContext
    {
        public DbSet<FormasPagamento> FormasPagamento { get; set; }
        public DbSet<Garagens> Garagens { get; set; }
        public DbSet<Passagens> Passagens { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<AuthenticationResponse> AuthenticationResponses { get; set; } // Adicione a classe AuthenticationResponse

        public GarageDbContext(DbContextOptions<GarageDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FormasPagamento>().HasNoKey();
            modelBuilder.Entity<Garagens>().HasKey(g => g.Codigo);
            modelBuilder.Entity<Passagens>().HasKey(p => p.Id);

            // Marque a classe AuthenticationResponse como uma entidade sem chave primária
            modelBuilder.Entity<AuthenticationResponse>().HasNoKey();
        }
    }
}
