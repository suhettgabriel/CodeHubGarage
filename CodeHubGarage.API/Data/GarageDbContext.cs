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
            // Configurar 'FormasPagamento' como uma entidade sem chave primária
            modelBuilder.Entity<FormasPagamento>().HasNoKey();

            // Defina as configurações de mapeamento das outras entidades aqui

            // Exemplo de configuração para 'Garagens' com chave primária
            modelBuilder.Entity<Garagens>().HasKey(g => g.Codigo);

            // Exemplo de configuração para 'Passagens' com chave primária
            modelBuilder.Entity<Passagens>().HasKey(p => p.Id);
        }
    }
}
