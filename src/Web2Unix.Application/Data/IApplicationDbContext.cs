using Microsoft.EntityFrameworkCore;
using Web2Unix.Domain.Entities;

namespace Web2Unix.Application.Data;

public interface IApplicationDbContext
{
    DbSet<WebRole> WebRoles { get; set; }

    DbSet<WebUserRole> WebUserRoles { get; set; }

    DbSet<Server> Servers { get; set; }

    DbSet<WebUser> WebUsers { get; set; }

    DbSet<AllowedConnection> AllowedConnections { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
