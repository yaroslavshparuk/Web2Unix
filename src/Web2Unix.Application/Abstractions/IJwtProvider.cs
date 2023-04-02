using Web2Unix.Domain.Entities;

namespace Web2Unix.Application.Abstractions;

public interface IJwtProvider
{
    string Generate(WebUser user, WebRole role);
}
