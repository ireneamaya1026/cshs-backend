using CSHSBackendAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSHSBackendAPI.Infrastructure.Data.Configurations;

public class ClearanceConfiguration : IEntityTypeConfiguration<Clearance>
{
    public void Configure(EntityTypeBuilder<Clearance> builder)
    {
        builder.HasOne(c => c.School)
            .WithMany()
            .HasForeignKey(c => c.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Campus)
            .WithMany()
            .HasForeignKey(c => c.CampusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Student)
            .WithMany()
            .HasForeignKey(c => c.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}