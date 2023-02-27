using Web2Unix.Domain.Exceptions;
using Web2Unix.Domain.Primitives;
namespace Web2Unix.Domain.ValueObjects;

public sealed class Hostname : ValueObject
{
    private const int _maxLength = 200;

    internal Hostname(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override IEnumerable<object> GetValues()
    {
        yield return Value;
    }

    public static Hostname Create(string hostname)
    {
        if (string.IsNullOrEmpty(hostname))
        {
            throw new CreationException("Hostname should be not empty");
        }

        if (hostname.Length > _maxLength)
        {
            throw new CreationException($"Hostname's max length is {_maxLength}");
        }

        return new Hostname(hostname);
    }
}