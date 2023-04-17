using MediatR;
using Web2Unix.Application.Abstractions;

namespace Web2Unix.Application.Terminal.Disconnect;

public class DisconnectCommandHandler : IRequestHandler<DisconnectCommand>
{
    private readonly IUnixTerminal _client;

    public DisconnectCommandHandler(IUnixTerminal client)
    {
        _client = client;
    }

    public Task Handle(DisconnectCommand request, CancellationToken cancellationToken)
    {
        _client.Disconnect(request);
        return Task.CompletedTask;
    }
}