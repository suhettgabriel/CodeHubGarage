using CodeHubGarage.API.Data;
using CodeHubGarage.API.Interface;
using CodeHubGarage.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class FechamentoService : IFechamentoService
{
    private readonly GarageDbContext _dbContext;

    public FechamentoService(GarageDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Passagens> FechamentoPorPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem)
    {
        var fechamento = _dbContext.Passagens
            .Where(e => e.DataHoraEntrada >= dataHoraInicial && e.DataHoraSaida <= dataHoraFinal && e.Garagem == codigoGaragem)
            .GroupBy(e => e.FormaPagamento)
            .Select(group => new Passagens
            {
                FormaPagamento = group.Key,
                QuantidadeTempo = group.Count(),
                PrecoTotal = group.Sum(e => e.PrecoTotal)
            })
            .ToList();

        return fechamento;
    }

    public void RegistrarSaidaEstacionamento(string userId, DateTime dataHoraSaida)
    {
        var estacionamentoAberto = _dbContext.Estacionamentos
            .FirstOrDefault(e => e.UserId == userId && e.Status && e.DataHoraSaida == null);

        if (estacionamentoAberto != null)
        {
            estacionamentoAberto.DataHoraSaida = dataHoraSaida;
            estacionamentoAberto.Status = false;

            _dbContext.SaveChanges();

            RegistrarPassagem(estacionamentoAberto);
        }
    }

    private void RegistrarPassagem(Estacionamentos estacionamento)
    {
        var passagem = new Passagens
        {
            Garagem = estacionamento.GaragemCodigo,
            CarroPlaca = estacionamento.CarroPlaca,
            CarroModelo = estacionamento.CarroModelo,
            CarroMarca = estacionamento.CarroMarca,
            DataHoraEntrada = estacionamento.DataHoraEntrada,
            DataHoraSaida = estacionamento.DataHoraSaida,
            FormaPagamento = estacionamento.FormasPagamentoCodigo,
            //PrecoTotal =
            //QuantidadeTempo =
        };

        _dbContext.Passagens.Add(passagem);
        _dbContext.SaveChanges();
    }
}
