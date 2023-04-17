using MediatR;
using Web2Unix.Application.Abstractions;

namespace Web2Unix.Application.Terminal.Command;

public class CommandCommandHandler : IRequestHandler<CommandCommand, string>
{
    private readonly IUnixTerminal _client;

    public CommandCommandHandler(IUnixTerminal client)
    {
        _client = client;
    }

    public async Task<string> Handle(CommandCommand request, CancellationToken cancellationToken)
    {
        return await _client.Execute(request);
    }
}