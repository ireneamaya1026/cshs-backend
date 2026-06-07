using System.Reflection;
using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // public DbSet<School> Schools { get; set; }
    public DbSet<Campus> Campuses { get; set; }
    public DbSet<SchoolYear> SchoolYears { get; set; }
    public DbSet<SystemUser> SystemUsers { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<EnrollmentStageHistory> EnrollmentStageHistories { get; set; }
    public DbSet<EnrollmentPayment> EnrollmentPayments { get; set; }
    public DbSet<WorkflowDefinition> WorkflowDefinitions { get; set; }
    public DbSet<WorkflowAudit> WorkflowAudits { get; set; }
    public DbSet<SubjectLoad> SubjectLoads { get; set; }
    public DbSet<BasicEdGrade> BasicEdGrades { get; set; }
    public DbSet<GradeActivity> GradeActivities { get; set; }
    public DbSet<StudentActivityScore> StudentActivityScores { get; set; }
    public DbSet<CollegeGrade> CollegeGrades { get; set; }
    public DbSet<GradeChangeRequest> GradeChangeRequests { get; set; }
    public DbSet<GradeChangeAudit> GradeChangeAudits { get; set; }
    public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
    public DbSet<DocumentRequest> DocumentRequests { get; set; }
    public DbSet<DocumentStatusHistory> DocumentStatusHistories { get; set; }
    public DbSet<Clearance> Clearances { get; set; }
    public DbSet<ClearanceDeptSignoff> ClearanceDeptSignoffs { get; set; }
    public DbSet<FeeStructure> FeeStructures { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<FormTemplate> FormTemplates { get; set; }
    public DbSet<PushToken> PushTokens { get; set; }

    public DbSet<SchoolConfig> SchoolConfigs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Fix all decimal precision warnings globally
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(12,2)");
        }

        // Fix cascade delete cycles — set all to Restrict globally
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Modified)
                entry.Entity.UpdatedAt = DateTime.UtcNow;
        }
        return base.SaveChangesAsync(ct);
    }
}