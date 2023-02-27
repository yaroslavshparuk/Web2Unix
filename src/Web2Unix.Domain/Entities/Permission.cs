using Web2Unix.Domain.Primitives;
namespace Web2Unix.Domain.Entities;

public sealed class Permission : Entity
{
    internal Permission(
        Guid id,
        string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; }

    public static Permission Create(
        Guid id,
        string name)
    {
        return new Permission(id, name);
    }
}