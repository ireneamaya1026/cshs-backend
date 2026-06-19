namespace CSHSBackendAPI.Application.Grades.DTOs;

public class CollegeGradeDto
{
    public long Id { get; set; }
    public long StudentId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string Semester { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string? Program { get; set; }
    public string? YearLevel { get; set; }
    public decimal? Prelim { get; set; }
    public decimal? Midterm { get; set; }
    public decimal? Finals { get; set; }
    public decimal? SemesterGrade { get; set; }
    public decimal? PointGrade { get; set; }
    public string? Descriptor { get; set; }
    public string? SpecialGrade { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? TeacherName { get; set; }
    public long? SubjectLoadId { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? RejectionNote { get; set; }
}