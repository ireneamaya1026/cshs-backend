namespace CSHSBackendAPI.Application.Students.DTOs;

public class StudentGradeDto
{
    public long Id { get; set; }
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
}