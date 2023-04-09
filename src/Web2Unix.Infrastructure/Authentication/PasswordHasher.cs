using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Web2Unix.Application.Abstractions;
using Web2Unix.Application.Data;

namespace Web2Unix.Infrastructure.Authentication;

public class PasswordHasher : IPasswordHasher
{
    private readonly IApplicationDbContext _context;

    public PasswordHasher(IApplicationDbContext context)
    {
        _context = context;
    }

    public (byte[] passwordHash, byte[] passwordSalt) Create(string password)
    {
        using (var hmac = new HMACSHA512())
            return (hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), hmac.Key);
    }

    public async Task<bool> Verify(int userId, string password)
    {
        var user = await _context.WebUsers.FirstOrDefaultAsync(x => x.Id == userId);
        using (var hmac = new HMACSHA512(user.PasswordSalt))
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password)).SequenceEqual(user.PasswordHash);
    }
}
