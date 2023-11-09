using CodeHubGarage.Domain;

namespace CodeHubGarage.API.Interface
{
    public interface IJwtSettings
    {
        string Key { get; }
        string Issuer { get; }
        string Audience { get; }
        double DurationInMinutes { get; }
    }
}