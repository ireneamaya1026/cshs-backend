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
            .HasMaxLength(200);

        builder.Property(c => c.CampusKey)
            .IsRequired()
            .HasMaxLength(30);

        builder.HasIndex(c => new { c.SchoolId, c.CampusKey })
            .IsUnique();

        builder.Property(c => c.Phone)
            .HasMaxLength(50);

        builder.Property(c => c.Email)
            .HasMaxLength(150);

        builder.HasMany(c => c.SystemUsers)
            .WithOne(u => u.Campus)
            .HasForeignKey(u => u.CampusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Students)
            .WithOne(s => s.Campus)
            .HasForeignKey(s => s.CampusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}