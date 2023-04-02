using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web2Unix.Domain.Entities;

namespace Web2Unix.Infrastructure.Configurations;

internal class WebUserRoleConfiguration : IEntityTypeConfiguration<WebUserRole>
{
    public void Configure(EntityTypeBuilder<WebUserRole> builder)
    {

        builder.HasKey(ur => new { ur.WebUserId, ur.WebRoleId });
        builder.Property(x => x.WebUserId);
        builder.Property(x => x.WebRoleId);
        ////builder.HasOne(x => x.WebUser.WebRole)
        ////    .WithOne(x => x.);
        builder.HasOne(ur => ur.WebUser)
            .WithMany(u => u.WebUserRoles)
            .HasForeignKey(ur => ur.WebUserId);

        builder.HasOne(ur => ur.WebRole)
            .WithMany(r => r.WebUserRoles)
            .HasForeignKey(ur => ur.WebRoleId);

        builder.HasData(WebUserRole.Create(1, 1));
    }
}