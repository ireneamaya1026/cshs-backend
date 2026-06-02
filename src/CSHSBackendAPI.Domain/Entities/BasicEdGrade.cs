using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class BasicEdGrade : BaseEntity
{
    public long SchoolId { get; set; }
    public long CampusId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public long StudentId { get; set; }
    public long? SubjectLoadId { get; set; }
    public long? TeacherId { get; set; }
    public string? TeacherName { get; set; }
    public string GradeLevel { get; set; } = string.Empty;
    public string? Section { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string? SubjectArea { get; set; }
    public GradePeriod Period { get; set; }
    public decimal? WrittenWorks { get; set; }
    public decimal? Performance { get; set; }
    public decimal? QuarterlyExam { get; set; }
    public decimal? InitialGrade { get; set; }
    public decimal? Transmuted { get; set; }
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