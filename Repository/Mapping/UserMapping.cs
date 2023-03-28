using Microsoft.EntityFrameworkCore;
using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.Username).IsRequired().HasMaxLength(428);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(428);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(248);
        builder.Property(x => x.ProfileImage).IsRequired().HasMaxLength(428);
        builder.Property(x => x.Type).IsRequired().HasMaxLength(128);

        builder.HasMany(x => x.Posts).WithOne(x => x.Owner);
        builder.HasMany(x => x.Friends).WithMany();
    }
}

