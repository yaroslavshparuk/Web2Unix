using Microsoft.EntityFrameworkCore;
using Web2Unix.Domain.Entities;

namespace Web2Unix.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Permission> Permissions { get; set; }

    public DbSet<Server> Servers { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=web2unix;Trusted_Connection=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}