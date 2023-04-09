using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;
using System.Collections.Concurrent;
using Web2Unix.Application.Abstractions;
using Web2Unix.Application.Data;
using Web2Unix.Application.Servers.Command;

namespace Web2Unix.Infrastructure.Connection;

public class UnixClient : IUnixClient
{
    private readonly IApplicationDbContext _context;
    private ConcurrentDictionary<(int userId, int serverId), SshClient> _openedClients;
    private readonly HubConnection _hubConnection;
    public UnixClient(IApplicationDbContext context)
    {
        _context = context;
        _openedClients = new ConcurrentDictionary<(int userId, int serverId), SshClient>();
        _hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:7123/terminalHub").Build();
    }

    public async Task Run(CommandRequest commandRequest)
    {
        var user = await _context.WebUsers.FirstOrDefaultAsync(x => x.Id == commandRequest.userId);
        var server = await _context.Servers.FirstOrDefaultAsync(x => x.Id == commandRequest.serverId);
        try
        {
            SshClient client;
            if(_openedClients.TryGetValue((commandRequest.userId, commandRequest.serverId), out var cachedClient)) {
                client = cachedClient;
            }
            else
            {
                var connectionInfo = new ConnectionInfo(server.IpAddress.Value, server.Port, user.Username.Value,
                new PasswordAuthenticationMethod(user.Username.Value, user.Username.Value));
                client = new SshClient(connectionInfo);
                client.Connect();
                _openedClients.TryAdd((commandRequest.userId, commandRequest.serverId), client);
            }

            if (client.IsConnected)
            {
                var command = client.RunCommand(commandRequest.commandValue);
                await _hubConnection.StartAsync();
                await _hubConnection.InvokeAsync("SendOutput", command.Result);
            }

            client.Disconnect();
            client.Dispose();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
