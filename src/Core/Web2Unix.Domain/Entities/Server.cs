using Web2Unix.Domain.Primitives;
namespace Web2Unix.Domain.Entities;

public sealed class Server : Entity
{
    public Server(
        Guid id,
        string hostname, 
        string ipAddress,
        short port, 
        DateTimeOffset createdAt, 
        DateTimeOffset updatedAt) 
        : base(id)
    {
        Hostname = hostname;
        IpAddress = ipAddress;
        Port = port;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public string Hostname { get; }

    public string IpAddress { get; }

    public short Port { get; }

    public DateTimeOffset CreatedAt { get; }

    public DateTimeOffset UpdatedAt { get; }

    public static Server Create(
        Guid id,
        string hostname,
        string ipAddress,
        short port,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt)
    {
        return new Server(id, hostname, ipAddress, port, createdAt, updatedAt);
    }
}