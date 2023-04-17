using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web2Unix.Application.Servers.Command;
using Web2Unix.Application.Servers.Connect;
using Web2Unix.Application.Servers.GetAll;

namespace Web2Unix.Presentation.Controllers;

[Route("api/server")]
[Authorize]
[ApiController]
public class ServerController : ControllerBase
{
    private readonly ISender _sender;

    public ServerController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var servers = (await _sender.Send(new GetAllCommand(), cancellationToken)).Select(x => new
        {
            x.Id,
            ServerName = x.ServerName.Value,
            IpAddress = x.IpAddress.Value,
            x.Port,
        }
        );
        return Ok(servers);
    }

    [HttpPost("connect")]
    public async Task<IActionResult> Connect([FromBody]ConnectRequest connectRequest, CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(new ConnectCommand(connectRequest.userId, connectRequest.serverId), cancellationToken));
    }

    [HttpPost("execute")]
    public async Task<IActionResult> Execute([FromBody] CommandRequest commandRequest, CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(new CommandCommand(commandRequest.userId, commandRequest.serverId, commandRequest.commandValue), cancellationToken));
    }
}