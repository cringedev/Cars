namespace Cars.Api.Options;

public class JwtOptions
{
    public int ExpiresInSeconds { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string Secret { get; set; }
}