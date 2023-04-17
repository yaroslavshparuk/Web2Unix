using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web2Unix.Application.Terminal.Command;
using Web2Unix.Application.Terminal.Connect;
using Web2Unix.Application.Terminal.Disconnect;

namespace Web2Unix.Presentation.Controllers;

[Route("api/terminal")]
[Authorize]
[ApiController]
public class TerminalController : ControllerBase
{
    private readonly ISender _sender;

    public TerminalController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("connect")]
    public async Task<IActionResult> Connect([FromBody]ConnectRequest connectRequest, CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(new ConnectCommand(connectRequest.userId, connectRequest.serverId), cancellationToken));
    }

    [HttpPost("execute")]
    public async Task<IActionResult> Execute([FromBody]CommandRequest commandRequest, CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(new CommandCommand(commandRequest.userId, commandRequest.serverId, commandRequest.commandValue), cancellationToken));
    }

    [HttpPost("disconnect")]
    public async Task<IActionResult> Disconnect([FromBody] DisconnectRequest disconnectRequest, CancellationToken cancellationToken)
    {
        await _sender.Send(new DisconnectCommand(disconnectRequest.userId, disconnectRequest.serverId), cancellationToken);
        return Ok();
    }
}