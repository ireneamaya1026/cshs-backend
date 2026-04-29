using CSHSBackendAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSHSBackendAPI.Infrastructure.Data.Configurations;

public class CampusConfiguration : IEntityTypeConfiguration<Campus>
{
    public void Configure(EntityTypeBuilder<Campus> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.Address)
            .HasMaxLength(255);

        builder.Property(c => c.ContactNumber)
            .HasMaxLength(20);

        // One campus has many users
        builder.HasMany(c => c.Users)
            .WithOne(u => u.Campus)
            .HasForeignKey(u => u.CampusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}