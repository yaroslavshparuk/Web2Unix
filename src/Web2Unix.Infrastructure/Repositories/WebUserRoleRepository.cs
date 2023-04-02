using Microsoft.EntityFrameworkCore;
using Web2Unix.Domain.Entities;
using Web2Unix.Domain.Repositories;

namespace Web2Unix.Infrastructure.Repositories;

public class WebUserRoleRepository : IWebUserRoleRepository
{
    public async Task<WebUserRole> Get(WebUser user)
    {
        using (var ctx = new ApplicationDbContext())
        {
            return await ctx.WebUserRoles
                .Include(x => x.WebRole)
                .Include(x => x.WebUser)
                .FirstOrDefaultAsync(x => x.WebUserId == user.Id);
        }
    }
}