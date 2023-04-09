using Web2Unix.Domain.Exceptions;
using Web2Unix.Domain.Primitives;
namespace Web2Unix.Domain.ValueObjects;

public sealed class ServerName : ValueObject
{
    private const int _maxLength = 200;

    private ServerName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override IEnumerable<object> GetValues()
    {
        yield return Value;
    }

    public static ServerName Create(string serverName)
    {
        if (string.IsNullOrEmpty(serverName))
        {
            throw new CreationException("Server name should be not empty");
        }

        if (serverName.Length > _maxLength)
        {
            throw new CreationException($"Server name's max length is {_maxLength}");
        }

        return new ServerName(serverName);
    }
}