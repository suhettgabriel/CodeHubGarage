using CodeHubGarage.API.Interface;
using CodeHubGarage.API.Service;
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

        public TempoMedioController(IGaragemService garagemService, IFormaPagamentoService formaPagamentoService, IPassagemService passagemService)
        {
            _garagemService = garagemService;
            _formaPagamentoService = formaPagamentoService;
            _passagemService = passagemService;
        }

        [HttpGet("tempo-medio-estadia-mensalistas")]
        public IActionResult TempoMedioEstadiaMensalistas(string codigoGaragem)
        {
            // Implemente a lógica para obter o tempo médio de estadia de mensalistas
            // Utilize os serviços e retorne os resultados
            // Exemplo: var tempoMedioMensalistas = _passagemService.GetTempoMedioEstadiaMensalistas(codigoGaragem);
            return Ok(/* tempoMedioMensalistas */);
        }

        [HttpGet("tempo-medio-estadia-clientes")]
        public IActionResult TempoMedioEstadiaClientes(string codigoGaragem)
        {
            // Implemente a lógica para obter o tempo médio de estadia de clientes não mensalistas
            // Utilize os serviços e retorne os resultados
            // Exemplo: var tempoMedioClientes = _passagemService.GetTempoMedioEstadiaClientes(codigoGaragem);
            return Ok(/* tempoMedioClientes */);
        }
    }

}
