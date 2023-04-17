using MediatR;

namespace Web2Unix.Application.Terminal.Connect;

public record ConnectCommand(int userId, int serverId) : IRequest<string>;