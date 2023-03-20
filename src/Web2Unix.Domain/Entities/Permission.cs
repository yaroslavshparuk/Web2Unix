using Web2Unix.Domain.Primitives;
namespace Web2Unix.Domain.Entities;

public class Permission : Entity
{
    internal Permission(
        int id,
        string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; }

    public static Permission Create(
        int id,
        string name)
    {
        return new Permission(id, name);
    }
}