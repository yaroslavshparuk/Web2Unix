using System.Security.Cryptography;
using System.Text;
using Web2Unix.Application.Abstractions;
using Web2Unix.Domain.Repositories;

namespace Web2Unix.Infrastructure.Authentication;

public class PasswordHasher : IPasswordHasher
{
    private readonly IWebUserRepository _webUserRepository;

    public PasswordHasher(IWebUserRepository webUserRepository)
    {
        _webUserRepository = webUserRepository;
    }

    public (byte[] passwordHash, byte[] passwordSalt) Create(string password)
    {
        using (var hmac = new HMACSHA512())
            return (hmac.Key, hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    public async Task<bool> Verify(int userId, string password)
    {
        var user = await _webUserRepository.Get(userId);
        using (var hmac = new HMACSHA512(user.PasswordSalt))
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password)).SequenceEqual(user.PasswordHash);
    }
}
