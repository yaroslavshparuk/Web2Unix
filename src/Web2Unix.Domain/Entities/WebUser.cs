using Web2Unix.Domain.Primitives;
using Web2Unix.Domain.ValueObjects;

namespace Web2Unix.Domain.Entities;

public class WebUser : Entity
{
    private WebUser(
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

    public ICollection<WebUserRole> WebUserRoles { get; set; }

    public static WebUser Create(
        int id,
        Username username,
        Password password,
        Email email,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt)
    {
        return new WebUser(id, username, password, email, createdAt, updatedAt);
    }
}
