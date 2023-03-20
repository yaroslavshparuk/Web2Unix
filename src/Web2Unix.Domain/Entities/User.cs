using Web2Unix.Domain.Primitives;
using Web2Unix.Domain.ValueObjects;

namespace Web2Unix.Domain.Entities;

public sealed class User : Entity
{
    internal User(
        int id,
        Username username,
        Password password,
        Email email,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt)
        : base(id)
    {
        Username = username;
        Password = password;
        Email = email;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Username Username { get; }

    public Password Password { get; }

    public Email Email { get; }

    public DateTimeOffset CreatedAt { get; }

    public DateTimeOffset UpdatedAt { get; }

    public static User Create(
        int id,
        Username username,
        Password password,
        Email email,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt)
    {
        return new User(id, username, password, email, createdAt, updatedAt);
    }
}
