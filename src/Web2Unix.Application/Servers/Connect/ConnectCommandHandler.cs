using MediatR;
using Microsoft.EntityFrameworkCore;
using Web2Unix.Application.Abstractions;
using Web2Unix.Application.Data;
using Web2Unix.Domain.Entities;

namespace Web2Unix.Application.Servers.Connect;

public class ConnectCommandHandler : IRequestHandler<ConnectCommand, ICollection<Server>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnixClient _unixClient;

    public ConnectCommandHandler(IApplicationDbContext context, IUnixClient unixClient)
    {
        _context = context;
        _unixClient = unixClient;
    }

    public async Task<ICollection<Server>> Handle(ConnectCommand request, CancellationToken cancellationToken)
    {
        await _unixClient.Connect(request.serverId, request.userId);
        return await _context.Servers.ToArrayAsync(cancellationToken);
    }
}