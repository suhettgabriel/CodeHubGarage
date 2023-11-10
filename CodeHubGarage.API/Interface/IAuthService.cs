using CodeHubGarage.Domain;
using Microsoft.AspNetCore.Identity;

namespace CodeHubGarage.API.Interface
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterRequest model);
        Task<AuthenticationResponse> LoginAsync(LoginRequest model);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
    }
}