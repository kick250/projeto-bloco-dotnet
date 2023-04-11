using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;

internal class PostMapping : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(248);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(448);
        builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(448);

        builder.HasMany(x => x.Comments).WithOne(x => x.Post);
    }
}
