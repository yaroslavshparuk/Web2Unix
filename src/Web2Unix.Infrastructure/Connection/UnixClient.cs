using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Web2Unix.Application.Abstractions;
using Web2Unix.Application.Data;
using Web2Unix.Application.Servers.Command;
using Web2Unix.Application.Servers.Connect;

namespace Web2Unix.Infrastructure.Connection;

public class UnixClient : IUnixClient
{
    private readonly IApplicationDbContext _context;
    private readonly IMemoryCache _memoryCache;
    private readonly HubConnection _hubConnection;

    public UnixClient(IApplicationDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
        //_hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:7123/terminalHub").Build();
    }

    public async Task<string> Connect(ConnectCommand connectCommand)
    {
        var user = await _context.WebUsers.FirstOrDefaultAsync(x => x.Id == connectCommand.userId);
        var server = await _context.Servers.FirstOrDefaultAsync(x => x.Id == connectCommand.serverId);
        var client = new MySshClient(server.IpAddress.Value, user.Username.Value, user.Username.Value);
        _memoryCache.Set((connectCommand.userId, connectCommand.serverId), client);
        //await _hubConnection.StartAsync();
        return await client.Connect();
    }

    public async Task<string> Execute(CommandCommand commandRequest)
    {
        if (_memoryCache.TryGetValue<MySshClient>((commandRequest.userId, commandRequest.serverId), out var client))
        {
            var res = await client.ExecuteCommand(commandRequest.commandValue);
            return res;
        }
        return "Something went wrong ...";
    }
}
