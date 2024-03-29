﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web2Unix.Domain.Entities;
using Web2Unix.Domain.ValueObjects;

namespace Web2Unix.Infrastructure.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<WebUser>
{
    public void Configure(EntityTypeBuilder<WebUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Username).HasConversion(x => x.Value, value => Username.Create(value));
        builder.Property(x => x.PasswordHash);
        builder.Property(x => x.PasswordSalt);
        builder.Property(x => x.Email).HasConversion(x => x.Value, value => Email.Create(value));
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);
        builder.HasData(WebUser.Create(1, Username.Create("yaroslav"), new byte[1], new byte[1], Email.Create("shparuk1996@gmail.com"), DateTime.UtcNow, DateTime.UtcNow));
    }
}