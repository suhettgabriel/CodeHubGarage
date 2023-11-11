using CodeHubGarage.API.Interface;
using CodeHubGarage.Domain;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CodeHubGarage.API.Controllers
{
    [Route("api/fechamento")]
    [ApiController]
    public class FechamentoController : ControllerBase
    {
        private readonly IGaragemService _garagemService;
        private readonly IFormaPagamentoService _formaPagamentoService;
        private readonly IPassagemService _passagemService;
        private readonly IEstacionamentoService _estacionamentoService;
        private readonly IFechamentoService _fechamentoService;

        public FechamentoController(IGaragemService garagemService, IFormaPagamentoService formaPagamentoService, IPassagemService passagemService, IEstacionamentoService estacionamentoService, IFechamentoService fechamentoService)
        {
            _garagemService = garagemService;
            _formaPagamentoService = formaPagamentoService;
            _passagemService = passagemService;
            _estacionamentoService = estacionamentoService;
            _fechamentoService = fechamentoService;
        }

        [HttpGet("fechamento-por-periodo")]
        public IActionResult FechamentoPorPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem)
        {
            var fechamento = _fechamentoService.FechamentoPorPeriodo(dataHoraInicial, dataHoraFinal, codigoGaragem);

            return Ok(fechamento);
        }

        [HttpPost("fechar-estacionamento")]
        public IActionResult FecharEstacionamento(string userId)
        {
            try
            {
                bool isMensalista = _estacionamentoService.VerificarSeUsuarioEhMensalista(userId);
                DateTime dataHoraSaida = DateTime.Now;
                TimeSpan quantidadeTempo = _estacionamentoService.BuscarQuantidadeTempo(userId, dataHoraSaida);
                decimal valorEstadia = _estacionamentoService.CalcularValorEstadia(userId, isMensalista, dataHoraSaida, dataHoraSaida);

                var dadosUsuario = _estacionamentoService.ObterDadosUsuario(userId);

                if (dadosUsuario == null)
                {
                    return BadRequest(new { Mensagem = "Usuário não tem carros estacionados na garagem." });
                }

                _estacionamentoService.RegistrarSaidaEstacionamento(userId, dataHoraSaida, dadosUsuario.Garagem, dadosUsuario.CarroPlaca, dadosUsuario.CarroMarca, dadosUsuario.CarroModelo, dataHoraSaida, dadosUsuario.FormaPagamento);
                _estacionamentoService.SalvarPassagem(userId, dataHoraSaida, _garagemService.GetGaragemByUserId(userId), dadosUsuario.CarroPlaca, dadosUsuario.CarroMarca, dadosUsuario.CarroModelo, dadosUsuario.FormaPagamento, quantidadeTempo, valorEstadia);

                return Ok(new { Mensagem = "Saída registrada com sucesso", ValorEstadia = valorEstadia });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Erro ao fechar o estacionamento: {ex.Message}" });
            }

        }
    }
}
