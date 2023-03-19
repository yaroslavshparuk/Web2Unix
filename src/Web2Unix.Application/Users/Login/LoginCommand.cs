using MediatR;

namespace Web2Unix.Application.Users.Login;

public record LoginCommand(string username, string password) : IRequest<string>;