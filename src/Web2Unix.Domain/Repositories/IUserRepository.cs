using System.Dynamic;
using Web2Unix.Domain.Entities;
using Web2Unix.Domain.ValueObjects;

namespace Web2Unix.Domain.Repositories;

public interface IUserRepository
{
    Task<User> Get(Username username, Password password);
}
