using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Infra.Persistence.Configurations;

public class ReferenceNumberConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        // Name is required and just 250 characters
        builder.Property(b => b.Id)
            .IsRequired();

    }
}