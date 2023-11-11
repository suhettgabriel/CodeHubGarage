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
            try
            {
                var mensalistas = _dbContext.Passagens
                    .Where(p => p.Garagem == codigoGaragem && p.DataHoraSaida != null && p.FormaPagamento == "Mensalista")
                    .AsEnumerable()  // Avalia o restante da consulta no lado do cliente
                    .GroupBy(p => p.CarroPlaca)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Max(p => p.DataHoraSaida.Value) - g.Min(p => p.DataHoraEntrada))
                    .DefaultIfEmpty(TimeSpan.Zero)
                    .Average(timeSpan => (decimal)timeSpan.TotalHours);

                return mensalistas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao calcular o tempo médio de estadia para mensalistas: {ex.Message}");
            }
        }


        public decimal GetTempoMedioEstadiaClientes(string codigoGaragem)
        {
            try
            {
                var clientes = _dbContext.Passagens
                    .Where(p => p.Garagem == codigoGaragem && p.DataHoraSaida != null && !_dbContext.Users.Any(u => u.CarroPlaca == p.CarroPlaca))
                    .AsEnumerable()  // Avalia o restante da consulta no lado do cliente
                    .GroupBy(p => p.CarroPlaca)
                    .Select(g => g.Max(p => p.DataHoraSaida.Value) - g.Min(p => p.DataHoraEntrada))
                    .DefaultIfEmpty(TimeSpan.Zero) // Se a lista estiver vazia, assume zero
                    .Average(timeSpan => (decimal)timeSpan.TotalHours);

                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao calcular o tempo médio de estadia: {ex.Message}");
            }
        }



    }
}
