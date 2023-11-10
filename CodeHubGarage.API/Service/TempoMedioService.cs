using CodeHubGarage.API.Data;
using CodeHubGarage.API.Interface;

namespace CodeHubGarage.API.Service
{
    public class TempoMedioService : ITempoMedioService
    {
        private readonly GarageDbContext _dbContext;

        public TempoMedioService(GarageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public decimal GetTempoMedioEstadiaMensalistas(string codigoGaragem)
        {
            var mensalistas = _dbContext.Passagens
                .Where(p => p.Garagem == codigoGaragem && p.DataHoraSaida != null)
                .GroupBy(p => p.CarroPlaca)
                .Where(g => g.Count() > 1)
                .Select(g => g.Max(p => p.DataHoraSaida.Value) - g.Min(p => p.DataHoraEntrada))
                .DefaultIfEmpty(TimeSpan.Zero)
                .Average(timeSpan => (decimal)timeSpan.TotalHours);

            return mensalistas;
        }

        public decimal GetTempoMedioEstadiaClientes(string codigoGaragem)
        {
            var clientes = _dbContext.Passagens
                .Where(p => p.Garagem == codigoGaragem && p.DataHoraSaida != null && !_dbContext.Users.Any(u => u.CarroPlaca == p.CarroPlaca))
                .GroupBy(p => p.CarroPlaca)
                .Select(g => g.Max(p => p.DataHoraSaida.Value) - g.Min(p => p.DataHoraEntrada))
                .DefaultIfEmpty(TimeSpan.Zero)
                .Average(timeSpan => (decimal)timeSpan.TotalHours);

            return clientes;
        }
    }
}
