using CodeHubGarage.API.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeHubGarage.API.Controllers
{
    [Route("api/tempo-medio")]
    [ApiController]
    public class TempoMedioController : ControllerBase
    {
        private readonly IGaragemService _garagemService;
        private readonly IFormaPagamentoService _formaPagamentoService;
        private readonly IPassagemService _passagemService;
        private readonly ITempoMedioService _tempoMedioService;

        public TempoMedioController(IGaragemService garagemService, IFormaPagamentoService formaPagamentoService, IPassagemService passagemService, ITempoMedioService tempoMedioService)
        {
            _garagemService = garagemService;
            _formaPagamentoService = formaPagamentoService;
            _passagemService = passagemService;
            _tempoMedioService = tempoMedioService;
        }

        [HttpGet("tempo-medio-estadia-mensalistas")]
        public IActionResult TempoMedioEstadiaMensalistas(string codigoGaragem)
        {
            var tempoMedioMensalistas = _tempoMedioService.GetTempoMedioEstadiaMensalistas(codigoGaragem);
            return Ok(tempoMedioMensalistas);
        }

        [HttpGet("tempo-medio-estadia-clientes")]
        public IActionResult TempoMedioEstadiaClientes(string codigoGaragem)
        {
            var tempoMedioClientes = _tempoMedioService.GetTempoMedioEstadiaClientes(codigoGaragem);
            return Ok(tempoMedioClientes);
        }
    }
}
