using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence.Configurations;

public class PamConfiguration : IEntityTypeConfiguration<Pam>
{
    public void Configure(EntityTypeBuilder<Pam> builder)
    {

        builder.Property(b => b.Category)
            .IsRequired();

        builder.Property(b => b.Description)
            .HasMaxLength(500);
    }
}