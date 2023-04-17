using MediatR;

namespace Web2Unix.Application.Terminal.Command;

public record CommandCommand(int userId, int serverId, string commandValue) : IRequest<string>;