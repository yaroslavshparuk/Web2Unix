using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web2Unix.Application.Abstractions;
using Web2Unix.Domain.Entities;

namespace Web2Unix.Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly IOptions<JwtOptions> _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options;
    }

    public string Generate(WebUser user, WebRole role)
    {
        var claims = new Claim[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email.Value.ToString()),
            new Claim(ClaimTypes.Role, role.Name),
        };

        var signCreds = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.Value.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _options.Value.Issuer,
            _options.Value.Audience,
            claims,
            null,
            DateTime.UtcNow.AddDays(1),
            signCreds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}