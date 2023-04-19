using Web2Unix.Domain.Exceptions;
using Web2Unix.Domain.Primitives;
namespace Web2Unix.Domain.ValueObjects;

public sealed class Username : ValueObject
{
    private const int _maxLength = 32;

    private Username(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override IEnumerable<object> GetValues()
    {
        yield return Value;
    }

    public static Username Create(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new CreationException("Username should be not empty");
        }

        if (username.Length > _maxLength)
        {
            throw new CreationException($"Username's max length is {_maxLength}");
        }

        return new Username(username);
    }
}

