using System.Text.RegularExpressions;
using Web2Unix.Domain.Exceptions;
using Web2Unix.Domain.Primitives;
namespace Web2Unix.Domain.ValueObjects;

public sealed class IpAddress : ValueObject
{
    private static Regex _IpRegex = new Regex("^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", RegexOptions.Compiled);

    internal IpAddress(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override IEnumerable<object> GetValues()
    {
        yield return Value;
    }

    public static IpAddress Create(string ipAddress)
    {
        if (string.IsNullOrEmpty(ipAddress))
        {
            throw new CreationException("IP Address should be not empty");
        }

        if (!_IpRegex.IsMatch(ipAddress))
        {
            throw new CreationException("IP Address is not valid");
        }

        return new IpAddress(ipAddress);
    }
}