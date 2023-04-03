using Microsoft.EntityFrameworkCore;
using Web2Unix.Domain.Entities;
using Web2Unix.Domain.Repositories;
using Web2Unix.Domain.ValueObjects;

namespace Web2Unix.Infrastructure.Repositories;

public class WebUserRepository : IWebUserRepository
{
    public async Task<WebUser> Get(int id)
    {
        using (var ctx = new ApplicationDbContext())
            return await ctx.WebUsers.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<WebUser> Get(Username username)
    {
        using (var ctx = new ApplicationDbContext())
            return await ctx.WebUsers.FirstOrDefaultAsync(x => x.Username == username);
    }

    //public async Task Update(int id, byte[] passwordHash, byte[] passwordSalt)
    //{
    //    using (var ctx = new ApplicationDbContext())
    //    {
    //        var user = ctx.WebUsers.FirstOrDefault(x => x.Id == id);
    //        user.PasswordHash = passwordHash;
    //        user.PasswordSalt = passwordSalt;
    //        await ctx.SaveChangesAsync();
    //    }
    //}
}