using Web2Unix.Application.Servers.Command;
using Web2Unix.Application.Servers.Connect;

namespace Web2Unix.Application.Abstractions;

public interface IUnixClient
{
    Task<string> Connect(ConnectCommand command);

    Task<string> Execute(CommandCommand command);
}
