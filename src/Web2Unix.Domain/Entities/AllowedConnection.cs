using Web2Unix.Domain.ValueObjects;

namespace Web2Unix.Domain.Entities;

public class AllowedConnection
{
    private AllowedConnection(int webUserId, int serverId, IpAddress fromIpAddress)
    {
        WebUserId = webUserId;
        ServerId = serverId;
        FromIpAddress = fromIpAddress;
    }

    public int WebUserId { get; }

    public int ServerId { get; }

    public IpAddress FromIpAddress { get; }

    public WebUser WebUser { get; }

    public Server Server { get; }

    public static AllowedConnection Create(int webUserId, int serverId, IpAddress fromIpAddress)
    {
        return new AllowedConnection(webUserId, serverId, fromIpAddress);
    }
}
