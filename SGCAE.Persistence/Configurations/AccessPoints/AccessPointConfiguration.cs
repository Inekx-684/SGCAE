using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGCAE.Domain.Entities.AccessPoints;
namespace SGCAE.Persistence.Configurations.AccessPoints;
public sealed class AccessPointConfiguration : IEntityTypeConfiguration<AccessPoint>
{
    public void Configure(EntityTypeBuilder<AccessPoint> builder)
    {
        builder.ToTable("AccessPoints");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Location).IsRequired().HasMaxLength(150);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}
