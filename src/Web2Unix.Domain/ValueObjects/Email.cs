using System.Text.RegularExpressions;
using Web2Unix.Domain.Exceptions;
using Web2Unix.Domain.Primitives;
namespace Web2Unix.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    private static Regex _emailRegex = new Regex("^\\S+@\\S+\\.\\S+$", RegexOptions.Compiled);
    internal Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override IEnumerable<object> GetValues()
    {
        yield return Value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new CreationException("Email should be not empty");
        }

        if (!_emailRegex.IsMatch(email))
        {
            throw new CreationException("Email is not valid");
        }

        return new Email(email);
    }
}