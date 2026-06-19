namespace CSHSBackendAPI.Application.Grades.DTOs;

public class BasicEdGradeDto
{
    public long Id { get; set; }
    public long StudentId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string GradeLevel { get; set; } = string.Empty;
    public string? Section { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string? SubjectArea { get; set; }
    public string Period { get; set; } = string.Empty;
    public decimal? WrittenWorks { get; set; }
    public decimal? Performance { get; set; }
    public decimal? QuarterlyExam { get; set; }
    public decimal? InitialGrade { get; set; }
    public decimal? Transmuted { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? TeacherName { get; set; }
    public long? TeacherId { get; set; }
    public long? SubjectLoadId { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? RejectionNote { get; set; }
}