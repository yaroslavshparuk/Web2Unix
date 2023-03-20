using Web2Unix.Domain.Primitives;
namespace Web2Unix.Domain.Entities;

public class Role : Entity
{
    public static readonly Role Registered = new()
    public Role(int id) : base(id)
    {
    }

    public int MyProperty { get; set; }
}
