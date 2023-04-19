using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Renci.SshNet;
using Web2Unix.Application.Abstractions;
using Web2Unix.Application.Data;
using Web2Unix.Application.Terminal.Command;
using Web2Unix.Application.Terminal.Connect;
using Web2Unix.Application.Terminal.Disconnect;

namespace Web2Unix.Infrastructure.ConnectionToUnix;

public class UnixTerminal : IUnixTerminal
{
    private readonly IApplicationDbContext _context;
    private readonly IMemoryCache _memoryCache;

    public UnixTerminal(IApplicationDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }

    public async Task<string> Connect(ConnectCommand connectCommand)
    {
        if (_memoryCache.TryGetValue<DurableSshClient>((connectCommand.userId, connectCommand.serverId), out var cachedClientConnection))
        {
            return cachedClientConnection.Output;
        }

        var user = await _context.WebUsers.FirstOrDefaultAsync(x => x.Id == connectCommand.userId);
        var server = await _context.Servers.FirstOrDefaultAsync(x => x.Id == connectCommand.serverId);
        var connectionInfo = new ConnectionInfo(server.IpAddress.Value, server.Port, user.Username.Value,
                new PasswordAuthenticationMethod(user.Username.Value, user.Username.Value));
        var clientConnection = new DurableSshClient(connectionInfo);
        _memoryCache.Set((connectCommand.userId, connectCommand.serverId), clientConnection);
        return await clientConnection.Open();
    }

    public void Disconnect(DisconnectCommand disconnectCommand)
    {
        if (_memoryCache.TryGetValue<DurableSshClient>((disconnectCommand.userId, disconnectCommand.serverId), out var clientConnection))
        {
            clientConnection.Dispose();
            _memoryCache.Remove((disconnectCommand.userId, disconnectCommand.serverId));
        }
    }

    public async Task<string> Execute(CommandCommand commandRequest)
    {
        if (_memoryCache.TryGetValue<DurableSshClient>((commandRequest.userId, commandRequest.serverId), out var clientConnection))
        {
            return await clientConnection.Execute(commandRequest.commandValue);
        }

        return "Something went wrong ...";
    }
}
