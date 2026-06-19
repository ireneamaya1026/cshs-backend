namespace CSHSBackendAPI.Application.GradeChanges.DTOs;

public class SubmitGradeChangeRequest
{
    public long StudentId { get; set; }
    public long CampusId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string GradeType { get; set; } = string.Empty;
    public long? BasicGradeId { get; set; }
    public long? CollegeGradeId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string? Period { get; set; }
    public decimal? OriginalGrade { get; set; }
    public decimal RequestedGrade { get; set; }
    public string Reason { get; set; } = string.Empty;
}