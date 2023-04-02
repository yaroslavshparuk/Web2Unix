using Microsoft.EntityFrameworkCore;
using Web2Unix.Domain.Entities;
using Web2Unix.Domain.Repositories;
using Web2Unix.Domain.ValueObjects;

namespace Web2Unix.Infrastructure.Repositories;

public class WebUserRepository : IWebUserRepository
{
    public async Task<WebUser> Get(Username username)
    {
        using (var ctx = new ApplicationDbContext())
        {
            return await ctx.WebUsers.FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}