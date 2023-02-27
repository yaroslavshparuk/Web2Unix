using Web2Unix.Domain.Exceptions;
using Web2Unix.Domain.Primitives;
namespace Web2Unix.Domain.ValueObjects;

public sealed class PermissionName : ValueObject
{
    private const int _maxLength = 100;

    internal PermissionName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override IEnumerable<object> GetValues()
    {
        yield return Value;
    }

    public static PermissionName Create(string permissionName)
    {
        if (string.IsNullOrEmpty(permissionName))
        {
            throw new CreationException("Permission Name should be not empty");
        }

        if (permissionName.Length > _maxLength)
        {
            throw new CreationException($"Permission Name's max length is {_maxLength}");
        }

        return new PermissionName(permissionName);
    }
}