namespace Web2Unix.Domain.Primitives;

public abstract class Enumeration<T> : IEquatable<Enumeration<T>>
{
    public int Value { get; protected init; }

    public string Name { get; protected init; }

    public bool Equals(Enumeration<T>? other)
    {
        throw new NotImplementedException();
    }
}
