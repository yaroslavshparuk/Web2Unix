using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web2Unix.Application.Servers.GetAll;

namespace Web2Unix.Presentation.Controllers;

[Route("api/server")]
[ApiController]
public class ServerController : ControllerBase
{
    private readonly ISender _sender;

    public ServerController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var servers = (await _sender.Send(new ConnectCommand(), cancellationToken)).Select(x => new
        {
            x.Id,
            ServerName = x.ServerName.Value,
            IpAddress = x.IpAddress.Value,
            x.Port,
        }
        );
        return Ok(servers);
    }
}