using MediatR;
using Web2Unix.Application.Abstractions;
using Web2Unix.Domain.Entities;
using Web2Unix.Domain.Exceptions;
using Web2Unix.Domain.Repositories;
using Web2Unix.Domain.ValueObjects;

namespace Web2Unix.Application.Users.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(IJwtProvider jwtProvider)
    {
        //_userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //var user = await _userRepository.Get(Username.Create(request.username), Password.Create(request.password));
        var user = User.Create(new Guid(), Username.Create(request.username), Password.Create(request.password), Email.Create("sdfsdf@gmail.com"), DateTime.UtcNow, DateTime.UtcNow);
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        return _jwtProvider.Generate(user);
    }
}