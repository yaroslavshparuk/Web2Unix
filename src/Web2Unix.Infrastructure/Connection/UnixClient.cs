using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;
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
        try
        {
            var connectionInfo = new ConnectionInfo("localhost", server.Port, user.Username.Value,
                new PasswordAuthenticationMethod(user.Username.Value, user.Username.Value));

            using (var client = new SshClient(connectionInfo))
            {
                client.Connect();

                if (client.IsConnected)
                {
                    var connection = new HubConnectionBuilder().WithUrl("https://localhost:7123/terminalHub").Build();
                    await connection.StartAsync();

                    var command = client.RunCommand("ls -la");
                    await connection.InvokeAsync("SendOutput", command.Result);
                }
                else
                {
                    Console.WriteLine("Failed to connect to SSH server.");
                }

                client.Disconnect();
                Console.WriteLine("SSH connection closed.");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
