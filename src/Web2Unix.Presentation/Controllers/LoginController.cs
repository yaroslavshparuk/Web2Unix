using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web2Unix.Application.Users.Login;

namespace Web2Unix.Presentation.Controllers;

[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ISender _sender;
    public LoginController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        return Ok("Test");
    }

    [HttpPost]
    public async Task<IActionResult> In(int id, [FromBody]LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.username, request.password);
        var token = await _sender.Send(command, cancellationToken);
        return Ok(token);
    }
}