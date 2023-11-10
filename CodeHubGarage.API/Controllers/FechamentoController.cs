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

        public FechamentoController(IGaragemService garagemService, IFormaPagamentoService formaPagamentoService, IPassagemService passagemService)
        {
            _garagemService = garagemService;
            _formaPagamentoService = formaPagamentoService;
            _passagemService = passagemService;
        }

        [HttpGet("fechamento-por-periodo")]
        public IActionResult FechamentoPorPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal, string codigoGaragem)
        {
            var fechamento = _passagemService.GetFechamentoPorPeriodo(dataHoraInicial, dataHoraFinal, codigoGaragem);

            return Ok(fechamento);
        }
    }
}
