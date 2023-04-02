using Web2Unix.Domain.Entities;

namespace Web2Unix.Domain.Repositories
{
    public interface IWebUserRoleRepository
    {
        Task<WebUserRole> Get(WebUser user);
    }
}
