namespace CSHSBackendAPI.Application.GradeChanges.DTOs;

public class GradeChangeDto
{
    public long Id { get; set; }
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
    public string RequestedByName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? ApprovedByName { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? RejectionNote { get; set; }
    public DateTime? PostedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}