using Microsoft.EntityFrameworkCore;
using Web2Unix.Application.Abstractions;
using Web2Unix.Application.Data;

namespace Web2Unix.Infrastructure.Connection;

public class UnixClient : IUnixClient
{
    private readonly IApplicationDbContext _context;

    public UnixClient(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Connect(int serverId, int userId)
    {
        var user = await _context.WebUsers.FirstOrDefaultAsync(x => x.Id == userId);
        var server = await _context.Servers.FirstOrDefaultAsync(x => x.Id == serverId);
    }
}
