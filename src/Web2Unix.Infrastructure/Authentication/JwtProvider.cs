using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web2Unix.Application.Abstractions;
using Web2Unix.Domain.Entities;

namespace Web2Unix.Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
    public string Generate(User user)
    {
        var claims = new Claim[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email.Value.ToString()),
        };

        var signCreds = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("init secret key, init secret key, init secret key")),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            user.Username.Value,
            "audience",
            claims,
            null,
            DateTime.UtcNow.AddDays(1),
            signCreds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}