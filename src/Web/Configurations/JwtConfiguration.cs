using Microsoft.Extensions.Options;
using Web2Unix.Infrastructure.Authentication;

namespace Web.Configurations;

public class JwtConfiguration : IConfigureOptions<JwtOptions>
{
    private const string _sectionName = "Jwt";
    private readonly IConfiguration _configuration;

    public JwtConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(_sectionName).Bind(options);
    }
}
