using Web2Unix.Domain.Exceptions;
using Web2Unix.Domain.Primitives;
namespace Web2Unix.Domain.ValueObjects;

public sealed class Password : ValueObject
{
    private const int _minLength = 8;
    private const int _maxLength = 255;

    internal Password(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override IEnumerable<object> GetValues()
    {
        yield return Value;
    }

    public static Password Create(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new CreationException("Password should be not empty");
        }

        if (password.Length < _minLength || password.Length > _maxLength)
        {
            throw new CreationException($"Password's length should be between {_minLength} and {_maxLength} characters");
        }

        return new Password(password);
    }
}