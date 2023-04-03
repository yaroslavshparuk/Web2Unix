namespace Web2Unix.Application.Abstractions;

public interface IPasswordHasher
{
    (byte[] passwordHash, byte[] passwordSalt) Create(string password);
    Task<bool> Verify(int userId, string password);
}
