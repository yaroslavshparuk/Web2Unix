﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web2Unix.Application.Users.Login;

namespace Web2Unix.Presentation.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.username, request.password);
        var token = await _sender.Send(command, cancellationToken);
        return Ok(token);
    }
}