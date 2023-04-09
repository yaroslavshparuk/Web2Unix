using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web2Unix.Application.Servers.Connect;
using Web2Unix.Application.Servers.GetAll;
using Web2Unix.Application.Users.Login;

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
    
    [HttpGet("connect/{userId}/{serverId}")]
    public async Task<IActionResult> Connect([FromRoute] ConnectRequest request,CancellationToken cancellationToken)
    {
        await _sender.Send(new ConnectCommand(request.userId, request.serverId), cancellationToken);
        return Ok();
    }
}