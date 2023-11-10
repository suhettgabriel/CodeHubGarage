using CodeHubGarage.API.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodeHubGarage.API.Controllers
{
    [Route("api/lista-carros")]
    [ApiController]
    public class ListaCarrosController : ControllerBase
    {
        private readonly IListaCarrosService _listaCarrosService;

        public ListaCarrosController(IListaCarrosService listaCarrosService)
        {
            _listaCarrosService = listaCarrosService;
        }

        [HttpGet("carros-no-periodo")]
        public IActionResult CarrosNoPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem)
        {
            var carrosNoPeriodo = _listaCarrosService.GetCarrosNoPeriodo(dataHoraInicial, dataHoraFinal, codigoGaragem);
            return Ok(carrosNoPeriodo);
        }

        [HttpGet("carros-na-garagem")]
        public IActionResult CarrosNaGaragem(string codigoGaragem)
        {
            var carrosNaGaragem = _listaCarrosService.GetCarrosNaGaragem(codigoGaragem);
            return Ok(carrosNaGaragem);
        }

        [HttpGet("carros-que-passaram")]
        public IActionResult CarrosQuePassaram(string codigoGaragem)
        {
            var carrosQuePassaram = _listaCarrosService.GetCarrosQuePassaram(codigoGaragem);
            return Ok(carrosQuePassaram);
        }
    }
}
