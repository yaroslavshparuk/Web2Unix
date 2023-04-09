using Renci.SshNet;
using Web2Unix.Application.Abstractions;

namespace Web2Unix.Infrastructure.Connection;

public class InMemoryActiveUnixConnections<TClient> : IActiveUnixConnections<TClient>
{
    public Task Add<TClient>(int userId, int serverId, TClient client)
    {
        throw new NotImplementedException();
    }
}
