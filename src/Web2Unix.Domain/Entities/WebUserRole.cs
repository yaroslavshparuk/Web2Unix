using Web2Unix.Domain.Primitives;

namespace Web2Unix.Domain.Entities;

public class WebUserRole 
{
    private WebUserRole(int webUserId, int webRoleId) 
    {
        WebUserId = webUserId;
        WebRoleId = webRoleId;
    }

    public int WebUserId { get; }

    public int WebRoleId { get; }

    public WebUser WebUser { get; }

    public WebRole WebRole { get; }

    public static WebUserRole Create(int webUserId, int webRoleId)
    {
        return new WebUserRole(webUserId, webRoleId);
    }
}
