using MediatR;
using Web2Unix.Application.Abstractions;

namespace Web2Unix.Application.Terminal.Connect;

public class ConnectCommandHandler : IRequestHandler<ConnectCommand, string>
{
    private readonly IUnixTerminal _client;

    public ConnectCommandHandler(IUnixTerminal client)
    {
        _client = client;
    }

    public async Task<string> Handle(ConnectCommand request, CancellationToken cancellationToken)
    {
        return await _client.Connect(request);
    }
}