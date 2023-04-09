using Web2Unix.Application.Servers.Command;

namespace Web2Unix.Application.Abstractions;

public interface IUnixClient
{
    Task Connect(int serverId, int userId);

    Task Run(CommandRequest command);
}
