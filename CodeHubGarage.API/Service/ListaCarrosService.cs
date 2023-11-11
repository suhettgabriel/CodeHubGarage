using CodeHubGarage.API.Data;
using CodeHubGarage.API.Interface;
using CodeHubGarage.Domain;

namespace CodeHubGarage.API.Service
{
    public class ListaCarrosService : IListaCarrosService
    {
        private readonly GarageDbContext _dbContext;

        public ListaCarrosService(GarageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Estacionamentos> GetCarrosNoPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem)
        {
            return _dbContext.Estacionamentos
                .Where(p => p.GaragemCodigo == codigoGaragem && p.DataHoraEntrada >= dataHoraInicial && p.DataHoraSaida <= dataHoraFinal)
                .ToList();
        }

        public List<Estacionamentos> GetCarrosNaGaragem(string codigoGaragem)
        {
            return _dbContext.Estacionamentos
                .Where(p => p.GaragemCodigo == codigoGaragem && p.DataHoraSaida == null)
                .ToList();
        }

        public List<Estacionamentos> GetCarrosQuePassaram(string codigoGaragem)
        {
            return _dbContext.Estacionamentos
                .Where(p => p.GaragemCodigo == codigoGaragem && p.DataHoraSaida != null)
                .ToList();
        }
    }
}
