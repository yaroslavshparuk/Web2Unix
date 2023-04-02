using Web2Unix.Domain.Entities;
using Web2Unix.Domain.ValueObjects;

namespace Web2Unix.Domain.Repositories;

public interface IWebUserRepository
{
    Task<WebUser> Get(Username username);
}