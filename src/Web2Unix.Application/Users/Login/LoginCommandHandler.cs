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
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(
        IJwtProvider jwtProvider, 
        IWebUserRepository webUserRepository,
        IWebUserRoleRepository webUserRoleRepository,
        IPasswordHasher passwordHasher)
    {
        _jwtProvider = jwtProvider;
        _webUserRepository = webUserRepository;
        _webUserRoleRepository = webUserRoleRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _webUserRepository.Get(Username.Create(request.username));
        if (user is null || !(await _passwordHasher.Verify(user.Id, request.password)))
        {
            throw new InvalidCredentialsException();
        }

        var role = await _webUserRoleRepository.Get(user);

        return _jwtProvider.Generate(user, role.WebRole);
    }
}