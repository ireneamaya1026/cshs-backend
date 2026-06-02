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
            .HasMaxLength(200);

        builder.Property(s => s.ShortName)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.Email)
            .HasMaxLength(150);

        builder.Property(s => s.Phone)
            .HasMaxLength(50);

        builder.Property(s => s.Plan)
            .HasConversion<string>();

        builder.Property(s => s.PortalBgStyle)
            .HasConversion<string>();

        builder.HasMany(s => s.Campuses)
            .WithOne(c => c.School)
            .HasForeignKey(c => c.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.SchoolYears)
            .WithOne(sy => sy.School)
            .HasForeignKey(sy => sy.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.SystemUsers)
            .WithOne(u => u.School)
            .HasForeignKey(u => u.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}