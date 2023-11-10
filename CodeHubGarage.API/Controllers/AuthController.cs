using CodeHubGarage.API.Interface;
using CodeHubGarage.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeHubGarage.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            var result = await _authService.RegisterAsync(model);

            if (result.Succeeded)
            {
                return Ok(new { Mensagem = "Usuário cadastrado com sucesso!" });
            }

            return BadRequest(new { Mensagem = "Não foi possivel cadastrar o usuário", Errors = result.Errors });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var response = await _authService.LoginAsync(model);

            if (response.Success)
            {
                return Ok(response);
            }

            return Unauthorized(new { Mensagem = "Autenticação falhou" });
        }
    }
}
