using Microsoft.EntityFrameworkCore;

namespace CodeHubGarage.API.Data
{
    public class GarageDbContext : DbContext
    {
        public DbSet<Garage> Garages { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Passage> Passages { get; set; }

        public GarageDbContext(DbContextOptions<GarageDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Defina as configurações de mapeamento das entidades aqui
        }
    }
}
