﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Infra.Persistence.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        // Name is required and just 250 characters
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(250);

        // Name is unique
        builder
        .HasIndex(b => b.Name)
        .IsUnique();

        // Acronym is required and just 250 characters
        builder.Property(b => b.Acronym)
            .IsRequired()
            .HasMaxLength(250);

        // Acronym is unique
        builder
        .HasIndex(b => b.Acronym)
        .IsUnique();
    }
}