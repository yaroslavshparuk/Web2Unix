using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Web2Unix.Application.Abstractions;
using Web2Unix.Application.Data;

namespace Web2Unix.Infrastructure.Access;

public class Firewall : IFirewall
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Firewall(IApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> CanAccess(int userId, int serverId)
    {
        var ipAddress = Domain.ValueObjects.IpAddress
            .Create(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
        return await _dbContext.AllowedConnections.AnyAsync(x => x.WebUserId == userId && x.ServerId == serverId && x.FromIpAddress == ipAddress);
    }
}