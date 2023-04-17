using Web2Unix.Application.Terminal.Command;
using Web2Unix.Application.Terminal.Connect;
using Web2Unix.Application.Terminal.Disconnect;

namespace Web2Unix.Application.Abstractions;

public interface IUnixTerminal
{
    Task<string> Connect(ConnectCommand command);

    Task<string> Execute(CommandCommand command);

    void Disconnect(DisconnectCommand command);
}
