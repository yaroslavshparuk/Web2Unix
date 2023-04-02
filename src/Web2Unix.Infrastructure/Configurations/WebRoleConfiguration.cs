using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web2Unix.Domain.Entities;
using Web2Unix.Domain.ValueObjects;

namespace Web2Unix.Infrastructure.Configurations;

internal class WebRoleConfiguration : IEntityTypeConfiguration<WebRole>
{
    public void Configure(EntityTypeBuilder<WebRole> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name);
        builder.HasData(WebRole.Create(1, "SuperAdmin"));
        builder.HasData(WebRole.Create(2, "Admin"));
        builder.HasData(WebRole.Create(3, "User"));
    }
}