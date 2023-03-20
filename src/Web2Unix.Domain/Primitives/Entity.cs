namespace Web2Unix.Domain.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    protected Entity(int id)
    {
        Id = id;
    }

    public int Id { get; }

    public static bool operator ==(Entity left, Entity right)
    {
        return left is not null && right is not null && left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType() || obj is not Entity entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    public bool Equals(Entity? other)
    {
        return other is not null && other.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 5;
    }
}
