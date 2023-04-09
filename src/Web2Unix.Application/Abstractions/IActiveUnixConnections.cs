namespace Web2Unix.Application.Abstractions;

public interface IActiveUnixConnections<TClient>
{
    Task Add<TClient>(int userId, int serverId, TClient client);
}
