using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class GradeChangeRequest : BaseEntity
{
    public long CampusId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public long StudentId { get; set; }
    public string GradeType { get; set; } = string.Empty;
    public long? BasicGradeId { get; set; }
    public long? CollegeGradeId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string? Period { get; set; }
    public decimal? OriginalGrade { get; set; }
    public decimal RequestedGrade { get; set; }
    public string Reason { get; set; } = string.Empty;
    public long RequestedById { get; set; }
    public string RequestedByName { get; set; } = string.Empty;
    public GradeChangeStatus Status { get; set; } = GradeChangeStatus.Requested;
    public long? ApprovedById { get; set; }
    public string? ApprovedByName { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? RejectionNote { get; set; }
    public long? PostedById { get; set; }
    public DateTime? PostedAt { get; set; }

    public Campus Campus { get; set; } = null!;
    public Student Student { get; set; } = null!;
    public SystemUser RequestedBy { get; set; } = null!;
    public BasicEdGrade? BasicGrade { get; set; }
    public CollegeGrade? CollegeGrade { get; set; }
    public ICollection<GradeChangeAudit> Audits { get; set; } = new List<GradeChangeAudit>();
}