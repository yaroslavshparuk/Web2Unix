﻿using Microsoft.AspNetCore.SignalR;
using Web2Unix.Application.Abstractions;
using Web2Unix.Application.Servers.Command;
using Web2Unix.Infrastructure.Connection;

namespace Web2Unix.Infrastructure.Hubs;

public class TerminalHub : Hub
{
    private readonly IUnixClient _unixClient;

    public TerminalHub(IUnixClient unixClient)
    {
        _unixClient = unixClient;
    }

    public async Task SendOutput(string output)
    {
        await Clients.All.SendAsync("output", output);
    }

    public async Task SendInput(CommandRequest input)
    {
        await _unixClient.Run(input);
        //await Clients.All.SendAsync("sendInput", input);
    }
}
