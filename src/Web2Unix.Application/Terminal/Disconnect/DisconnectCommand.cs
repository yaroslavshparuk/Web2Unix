using MediatR;

namespace Web2Unix.Application.Terminal.Disconnect;

public record DisconnectCommand(int userId, int serverId) : IRequest;