using CodeHubGarage.API.Interface;
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
                //PRECISA VIM COM A REQUISIÇÃO O CODIGO DA GARAGEM
                bool isMensalista = _estacionamentoService.VerificarSeUsuarioEhMensalista(userId);
                DateTime dataHoraSaida = DateTime.Now;

                //vERIFICAR SE O STATUS É DIFERENTE DE 0, SE FOR CONTINUA, SENAO, INFORMA QUE O USUÁRIO NÃO TEM CARRO ESTACIONADO NA GARAGEM
                decimal valorEstadia = _estacionamentoService.CalcularValorEstadia(userId, isMensalista, dataHoraSaida,dataHoraSaida );
                _fechamentoService.RegistrarSaidaEstacionamento(userId, dataHoraSaida);
                
                return Ok(new { Mensagem = "Saída registrada com sucesso", ValorEstadia = valorEstadia });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Erro ao fechar o estacionamento: {ex.Message}" });
            }
        }
    }
}
