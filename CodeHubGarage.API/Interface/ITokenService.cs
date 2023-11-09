using CodeHubGarage.Domain;

namespace CodeHubGarage.API.Interface
{
    public interface ITokenService
    {
        TokenResponse GenerateToken(ApplicationUser user);
    }
}
