using Microsoft.EntityFrameworkCore;
using Web2Unix.Application.Data;
using Web2Unix.Domain.Entities;

namespace Web2Unix.Infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<WebRole> WebRoles { get; set; }

    public DbSet<WebUserRole> WebUserRoles { get; set; }

    public DbSet<Server> Servers { get; set; }

    public DbSet<WebUser> WebUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=web2unix;Trusted_Connection=True;TrustServerCertificate=Yes");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}