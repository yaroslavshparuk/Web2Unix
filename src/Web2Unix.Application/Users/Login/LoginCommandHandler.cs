using MediatR;
using Microsoft.EntityFrameworkCore;
using Web2Unix.Application.Abstractions;
using Web2Unix.Application.Data;
using Web2Unix.Domain.Exceptions;
using Web2Unix.Domain.ValueObjects;

namespace Web2Unix.Application.Users.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(
        IJwtProvider jwtProvider,
        IApplicationDbContext context,
        IPasswordHasher passwordHasher)
    {
        _jwtProvider = jwtProvider;
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.WebUsers.FirstOrDefaultAsync(x => x.Username == Username.Create(request.username));
        if (user is null || !await _passwordHasher.Verify(user.Id, request.password))
        {
            throw new InvalidCredentialsException(); 
        }

        var role = await _context.WebUserRoles
                .Include(x => x.WebRole)
                .Include(x => x.WebUser)
                .FirstOrDefaultAsync(x => x.WebUserId == user.Id);

        return _jwtProvider.Generate(user, role.WebRole);
    }
}