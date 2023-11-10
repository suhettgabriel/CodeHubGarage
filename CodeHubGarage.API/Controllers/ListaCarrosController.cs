using CodeHubGarage.API.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeHubGarage.API.Controllers
{
    [Route("api/lista-carros")]
    [ApiController]
    public class ListaCarrosController : ControllerBase
    {
        private readonly IGaragemService _garagemService;
        private readonly IFormaPagamentoService _formaPagamentoService;
        private readonly IPassagemService _passagemService;

        public ListaCarrosController(IGaragemService garagemService, IFormaPagamentoService formaPagamentoService, IPassagemService passagemService)
        {
            _garagemService = garagemService;
            _formaPagamentoService = formaPagamentoService;
            _passagemService = passagemService;
        }

        [HttpGet("carros-no-periodo")]
        public IActionResult CarrosNoPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem)
        {
            // Implemente a lógica para obter carros no período
            // Utilize os serviços e retorne os resultados
            // Exemplo: var carrosNoPeriodo = _passagemService.GetCarrosNoPeriodo(dataHoraInicial, dataHoraFinal, codigoGaragem);
            return Ok(/* carrosNoPeriodo */);
        }

        [HttpGet("carros-na-garagem")]
        public IActionResult CarrosNaGaragem(string codigoGaragem)
        {
            // Implemente a lógica para obter carros na garagem
            // Utilize os serviços e retorne os resultados
            // Exemplo: var carrosNaGaragem = _passagemService.GetCarrosNaGaragem(codigoGaragem);
            return Ok(/* carrosNaGaragem */);
        }

        [HttpGet("carros-que-passaram")]
        public IActionResult CarrosQuePassaram(string codigoGaragem)
        {
            // Implemente a lógica para obter carros que passaram
            // Utilize os serviços e retorne os resultados
            // Exemplo: var carrosQuePassaram = _passagemService.GetCarrosQuePassaram(codigoGaragem);
            return Ok(/* carrosQuePassaram */);
        }
    }

}
