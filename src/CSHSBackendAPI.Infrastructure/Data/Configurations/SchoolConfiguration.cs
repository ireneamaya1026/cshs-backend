using CSHSBackendAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSHSBackendAPI.Infrastructure.Data.Configurations;

public class SchoolConfiguration : IEntityTypeConfiguration<School>
{
    public void Configure(EntityTypeBuilder<School> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(s => s.Slug)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(s => s.Slug)
            .IsUnique();   // no two schools can have the same slug

        builder.Property(s => s.Address)
            .HasMaxLength(255);

        builder.Property(s => s.ContactEmail)
            .HasMaxLength(150);

        builder.Property(s => s.ContactNumber)
            .HasMaxLength(20);

        // One school has many campuses
        builder.HasMany(s => s.Campuses)
            .WithOne(c => c.School)
            .HasForeignKey(c => c.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}