using MediatR;
using Web2Unix.Application.Abstractions;
using Web2Unix.Domain.Entities;
using Web2Unix.Domain.Exceptions;
using Web2Unix.Domain.Repositories;
using Web2Unix.Domain.ValueObjects;

namespace Web2Unix.Application.Users.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IWebUserRepository _webUserRepository;
    private readonly IWebUserRoleRepository _webUserRoleRepository;

    public LoginCommandHandler(
        IJwtProvider jwtProvider, 
        IWebUserRepository webUserRepository,
        IWebUserRoleRepository webUserRoleRepository)
    {
        _jwtProvider = jwtProvider;
        _webUserRepository = webUserRepository;
        _webUserRoleRepository = webUserRoleRepository;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _webUserRepository.Get(Username.Create(request.username));
        //var user = WebUser.Create(new Random().Next(1, 100), Username.Create(request.username), Password.Create(request.password), Email.Create("sdfsdf@gmail.com"), DateTime.UtcNow, DateTime.UtcNow);
        if (user is null || user.Password.Value != request.password)
        {
            throw new InvalidCredentialsException();
        }

        var role = await _webUserRoleRepository.Get(user);

        return _jwtProvider.Generate(user, role.WebRole);
    }
}