using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web2Unix.Domain.Entities;
using Web2Unix.Domain.ValueObjects;

namespace Web2Unix.Infrastructure.Configurations;

internal class ServerConfiguration : IEntityTypeConfiguration<Server>
{
    public void Configure(EntityTypeBuilder<Server> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ServerName).HasConversion(x => x.Value, value => ServerName.Create(value));
        builder.Property(x => x.IpAddress).HasConversion(x => x.Value, value => IpAddress.Create(value));
        builder.Property(x => x.Port);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);
    }
}