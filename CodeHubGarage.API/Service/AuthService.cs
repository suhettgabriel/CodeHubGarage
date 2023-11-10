using CodeHubGarage.API.Interface;
using CodeHubGarage.Domain;
using Microsoft.AspNetCore.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService,
        ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterRequest model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Username,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DateOfBirth = model.DateOfBirth,
            PhoneNumber = model.PhoneNumber,
            CarroPlaca = model.CarroPlaca,
            IsMensalista = model.IsMensalista,
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        return result;
    }

    public async Task<AuthenticationResponse> LoginAsync(LoginRequest model)
    {
        _logger.LogInformation($"Tentando logar o usuário: {model.Username}");

        var user = await _userManager.FindByNameAsync(model.Username);

        if (user == null)
        {
            _logger.LogError($"Usuário não encontrado: {model.Username}");
            return new AuthenticationResponse { Success = false, Errors = new List<string> { "Usuário não encontrado" } };
        }

        _logger.LogInformation($"Usuário encontrado: {user.UserName}");

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        if (result.Succeeded)
        {
            _logger.LogInformation($"Usuário {user.UserName} autenticado com sucesso.");

            var token = _tokenService.GenerateToken(user);
            var expirationDateTime = DateTime.UtcNow.AddSeconds(token.ExpiresIn);

            return new AuthenticationResponse
            {
                UserId = user.Id,
                Username = user.UserName,
                Token = token.Token,
                RefreshToken = token.RefreshToken,
                ExpiresIn = expirationDateTime,
                Success = true,
                Errors = null
            };
        }

        _logger.LogWarning($"Login falhou para o usuário: {user.UserName}");

        return new AuthenticationResponse { Success = false, Errors = new List<string> { "Login falhou" } };
    }

    public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
    {
        var result = await _userManager.AddToRoleAsync(user, role);

        return result;
    }
}
