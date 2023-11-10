using CodeHubGarage.API.Interface;
using CodeHubGarage.API.Data;
using CodeHubGarage.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class PassagemService : IPassagemService
{
    private readonly GarageDbContext _dbContext;

    public PassagemService(GarageDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Passagens> GetPassagensByGaragem(string codigoGaragem)
    {
        return _dbContext.Passagens
            .Where(p => p.Garagem == codigoGaragem)
            .ToList();
    }

    public List<Passagens> GetPassagensByPeriod(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem)
    {
        return _dbContext.Passagens
            .Where(p => p.Garagem == codigoGaragem && p.DataHoraEntrada >= dataHoraInicial && p.DataHoraSaida <= dataHoraFinal)
            .ToList();
    }

    public List<Passagens> GetCarrosNoPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem)
    {
        return _dbContext.Passagens
            .Where(p => p.Garagem == codigoGaragem && p.DataHoraEntrada >= dataHoraInicial && p.DataHoraSaida == null)
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

    public List<Passagens> GetFechamentoPorPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem)
    {
        return _dbContext.Passagens
            .Where(p => p.Garagem == codigoGaragem && p.DataHoraEntrada >= dataHoraInicial && p.DataHoraSaida <= dataHoraFinal)
            .ToList();
    }

    public List<Passagens> GetTempoMedioEstadiaMensalistas(string codigoGaragem)
    {
        return _dbContext.Passagens
            .Where(p => p.Garagem == codigoGaragem && p.FormaPagamento == "Mensalista")
            .ToList();
    }

    public List<Passagens> GetTempoMedioEstadiaClientes(string codigoGaragem)
    {
        return _dbContext.Passagens
            .Where(p => p.Garagem == codigoGaragem && p.FormaPagamento != "Mensalista" && p.DataHoraSaida != null)
            .ToList();
    }
}
