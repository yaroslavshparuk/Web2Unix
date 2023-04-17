using MediatR;
namespace Web2Unix.Application.Servers.Connect;

public record ConnectCommand(int userId, int serverId) : IRequest<string>;