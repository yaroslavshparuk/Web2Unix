using MediatR;
using Web2Unix.Application.Abstractions;

namespace Web2Unix.Application.Servers.Connect;

public class ConnectCommandHandler : IRequestHandler<ConnectCommand, string>
{
    private readonly IUnixClient _client;

    public ConnectCommandHandler(IUnixClient client)
    {
        _client = client;
    }

    public async Task<string> Handle(ConnectCommand request, CancellationToken cancellationToken)
    {
        return await _client.Connect(request);
    }
}