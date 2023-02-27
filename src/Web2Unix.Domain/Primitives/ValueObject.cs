namespace Web2Unix.Domain.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetValues();

    public override bool Equals(object obj)
    {
        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }

        return obj is ValueObject other && ValuesOfObjectsEqual(other);
    }

    public bool Equals(ValueObject? other)
    {
        return other is not null && ValuesOfObjectsEqual(other);
    }

    public override int GetHashCode()
    {
        return GetValues().Aggregate(default(int), HashCode.Combine);
    }

    private bool ValuesOfObjectsEqual(ValueObject other)
    {
        return GetValues().SequenceEqual(other.GetValues());
    }
}