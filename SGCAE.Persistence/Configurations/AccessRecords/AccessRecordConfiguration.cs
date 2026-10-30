using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGCAE.Domain.Entities.AccessRecords;
namespace SGCAE.Persistence.Configurations.AccessRecords;
public sealed class AccessRecordConfiguration : IEntityTypeConfiguration<AccessRecord>
{
    public void Configure(EntityTypeBuilder<AccessRecord> builder)
    {
        builder.ToTable("AccessRecords");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.CredentialCode).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Reason).HasMaxLength(250).IsRequired();
        builder.Property(x => x.Movement).HasConversion<string>().HasMaxLength(15).IsRequired();
        builder.Property(x => x.Decision).HasConversion<string>().HasMaxLength(15).IsRequired();
        builder.Property(x => x.OccurredAt).IsRequired();
        builder.HasIndex(x => new { x.EmployeeId, x.OccurredAt });
        builder.HasIndex(x => new { x.AccessPointId, x.OccurredAt });
        builder.HasIndex(x => new { x.Decision, x.OccurredAt });
        // IDs informativos sin FK: conservamos el historial aunque se retiren credenciales o puntos.
    }
}
