namespace Web2Unix.Application.Abstractions;

public interface IUnixClient
{
    Task Connect(int serverId, int userId);
}
