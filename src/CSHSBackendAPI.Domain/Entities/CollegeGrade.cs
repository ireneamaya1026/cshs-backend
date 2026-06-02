using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class CollegeGrade : BaseEntity
{
    public long SchoolId { get; set; }
    public long CampusId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string Semester { get; set; } = string.Empty;
    public long StudentId { get; set; }
    public long? SubjectLoadId { get; set; }
    public long? TeacherId { get; set; }
    public string? TeacherName { get; set; }
    public string? Program { get; set; }
    public string? YearLevel { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public decimal? Prelim { get; set; }
    public decimal? Midterm { get; set; }
    public decimal? Finals { get; set; }
    public decimal? SemesterGrade { get; set; }
    public decimal? PointGrade { get; set; }
    public string? Descriptor { get; set; }
    public string? SpecialGrade { get; set; }
    public DateOnly? IncDeadline { get; set; }
    public DateTime? IncResolvedAt { get; set; }
    public GradeStatus Status { get; set; } = GradeStatus.Draft;
    public DateTime? SubmittedAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public long? ApprovedById { get; set; }
    public string? RejectionNote { get; set; }

    // Relationships
    public School School { get; set; } = null!;
    public Campus Campus { get; set; } = null!;
    public Student Student { get; set; } = null!;
    public SubjectLoad? SubjectLoad { get; set; }
    public SystemUser? Teacher { get; set; }
}