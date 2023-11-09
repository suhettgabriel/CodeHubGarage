using CodeHubGarage.API.Interface;
using CodeHubGarage.Domain;
using Microsoft.AspNetCore.Identity;


public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterRequest model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Username,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DateOfBirth = model.DateOfBirth,
            PhoneNumber = model.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        return result;
    }

    public async Task<AuthenticationResponse> LoginAsync(LoginRequest model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user == null)
        {
            return new AuthenticationResponse { Success = false, Errors = new List<string> { "User not found" } };
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        if (result.Succeeded)
        {
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

        return new AuthenticationResponse { Success = false, Errors = new List<string> { "Login failed" } };
    }


    public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
    {
        var result = await _userManager.AddToRoleAsync(user, role);

        return result;
    }
}