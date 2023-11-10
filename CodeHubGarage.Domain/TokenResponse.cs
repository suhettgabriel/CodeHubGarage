namespace CodeHubGarage.Domain
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public double ExpiresIn { get; set; }
    }
}