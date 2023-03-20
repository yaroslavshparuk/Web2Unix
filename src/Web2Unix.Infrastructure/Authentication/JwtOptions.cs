namespace Web2Unix.Infrastructure.Authentication;

public class JwtOptions
{
    public string Issuer { get; init; } = "Web2Unix";

    public string Audience { get; init; } = "Web2Unix";

    public string SecretKey { get; init; } = "Web2Unix secret key";
}
