using MediatR;
using Microsoft.EntityFrameworkCore;
using Web2Unix.Application.Abstractions;
using Web2Unix.Application.Servers.Connect;

namespace Web2Unix.Application.Servers.Command;

public class CommandCommandHandler : IRequestHandler<CommandCommand, string>
{
    private readonly IUnixClient _client;

    public CommandCommandHandler(IUnixClient client)
    {
        _client = client;
    }

    public async Task<string> Handle(CommandCommand request, CancellationToken cancellationToken)
    {
        return await _client.Execute(request);
    }
}