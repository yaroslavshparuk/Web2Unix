using Web2Unix.Domain.Primitives;

namespace Web2Unix.Domain.Entities;

public class WebRole : Entity
{
    private WebRole(int id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; }

    public ICollection<WebUserRole> WebUserRoles { get; set; }

    public static WebRole Create(int id, string name)
    {
        return new WebRole(id, name);
    }
}
