﻿using CodeHubGarage.API.Data;
using CodeHubGarage.API.Interface;
using CodeHubGarage.Domain;
using Microsoft.AspNetCore.Mvc;

[Route("api/estacionamento")]
[ApiController]
public class EstacionamentoController : ControllerBase
{
    private readonly IEstacionamentoService _estacionamentoService;
    private readonly GarageDbContext _dbContext;
    private readonly IGaragemService _garagemService;
    private readonly ILogger<EstacionamentoController> _logger;
    private List<Estacionamentos> listaEstacionamentos = new List<Estacionamentos>();

    public EstacionamentoController(
        IEstacionamentoService estacionamentoService,
        GarageDbContext dbContext,
        IGaragemService garagemService,
        ILogger<EstacionamentoController> logger)
    {
        _estacionamentoService = estacionamentoService;
        _dbContext = dbContext;
        _garagemService = garagemService;
        _logger = logger;
    }

    [HttpPost("estacionar")]
    public IActionResult EstacionarCarro([FromBody] EstacionamentoRequest request)
    {
        var entrada = DateTime.Now;
        var userId = request.UserId;
        var userIsMensalista = _estacionamentoService.VerificarSeUsuarioEhMensalista(userId);
        var nomeUsuario = _estacionamentoService.VerificaNomeUsuario(userId);
        var mensagemRetorno = $"Carro estacionado com sucesso! {nomeUsuario} entrou às {entrada}";

        _logger.LogInformation($"Usuário {nomeUsuario} está estacionando um carro.");

        var codGaragem = request.GaragemCodigo;

        var estacionamento = new Estacionamentos
        {
            UserId = userId,
            GaragemCodigo = codGaragem,
            DataHoraEntrada = entrada,
            FormasPagamentoCodigo = request.FormasPagamentoCodigo,
            Status = false,
            isMensalista = userIsMensalista,
            DataHoraSaida = null
        };

        listaEstacionamentos.Add(estacionamento);

        SalvarEntradaEstacionamento(estacionamento);

        return Ok(new { Mensagem = mensagemRetorno });
    }

    private void SalvarEntradaEstacionamento(Estacionamentos estacionamento)
    {
        var novoEstacionamento = new Estacionamentos
        {
            UserId = estacionamento.UserId,
            GaragemCodigo = estacionamento.GaragemCodigo,
            FormasPagamentoCodigo = estacionamento.FormasPagamentoCodigo,
            DataHoraEntrada = estacionamento.DataHoraEntrada,
            Status = false,
            isMensalista = estacionamento.isMensalista,
        };

        _dbContext.Estacionamentos.Add(novoEstacionamento);
        _dbContext.SaveChanges();
    }
}
