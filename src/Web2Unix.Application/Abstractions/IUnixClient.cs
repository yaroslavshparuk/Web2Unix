using Web2Unix.Application.Servers.Command;

namespace Web2Unix.Application.Abstractions;

public interface IUnixClient
{
    Task Run(CommandRequest command);
}
