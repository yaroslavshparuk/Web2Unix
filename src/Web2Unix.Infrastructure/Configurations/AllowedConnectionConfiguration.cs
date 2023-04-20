using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Web2Unix.Domain.Entities;
using Web2Unix.Domain.ValueObjects;

namespace Web2Unix.Infrastructure.Configurations;

internal class AllowedConnectionConfiguration : IEntityTypeConfiguration<AllowedConnection>
{
    public void Configure(EntityTypeBuilder<AllowedConnection> builder)
    {
        builder.HasKey(ur => new { ur.WebUserId, ur.ServerId });
        builder.Property(x => x.WebUserId);
        builder.Property(x => x.ServerId);
        builder.Property(x => x.FromIpAddress).HasConversion(x => x.Value, value => IpAddress.Create(value));
        builder.HasOne(ur => ur.WebUser)
            .WithMany(u => u.AllowedConnections)
            .HasForeignKey(ur => ur.WebUserId);

        builder.HasOne(ur => ur.Server)
            .WithMany(r => r.AllowedForUsers)
            .HasForeignKey(ur => ur.ServerId);
        builder.HasData(AllowedConnection.Create(1, 1, IpAddress.Create("127.0.0.1")));
    }
}