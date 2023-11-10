using CodeHubGarage.API.Data;
using CodeHubGarage.API.Interface;
using CodeHubGarage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeHubGarage.API.Service
{
    public class ListaCarrosService : IListaCarrosService
    {
        private readonly GarageDbContext _dbContext;

        public ListaCarrosService(GarageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Passagens> GetCarrosNoPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem)
        {
            return _dbContext.Passagens
                .Where(p => p.Garagem == codigoGaragem && p.DataHoraEntrada >= dataHoraInicial && p.DataHoraSaida <= dataHoraFinal)
                .ToList();
        }

        public List<Passagens> GetCarrosNaGaragem(string codigoGaragem)
        {
            return _dbContext.Passagens
                .Where(p => p.Garagem == codigoGaragem && p.DataHoraSaida == null)
                .ToList();
        }

        public List<Passagens> GetCarrosQuePassaram(string codigoGaragem)
        {
            return _dbContext.Passagens
                .Where(p => p.Garagem == codigoGaragem && p.DataHoraSaida != null)
                .ToList();
        }
    }
}
