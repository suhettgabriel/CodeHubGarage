using CodeHubGarage.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodeHubGarage.API.Data
{
    public class GarageDbContext : DbContext
    {
        public DbSet<FormasPagamento> FormasPagamento { get; set; }
        public DbSet<Garagens> Garagens { get; set; }
        public DbSet<Passagens> Passagens { get; set; }

        public GarageDbContext(DbContextOptions<GarageDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Defina as configurações de mapeamento das entidades aqui
        }
    }
}
