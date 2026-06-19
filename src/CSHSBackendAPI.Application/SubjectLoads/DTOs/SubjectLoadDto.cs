namespace CSHSBackendAPI.Application.SubjectLoads.DTOs;

public class SubjectLoadDto
{
    public long Id { get; set; }
    public long CampusId { get; set; }
    public string CampusName { get; set; } = string.Empty;
    public string SchoolYear { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public long TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public string? GradeLevel { get; set; }
    public string? Section { get; set; }
    public string? SubjectArea { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string? Program { get; set; }
    public string? YearLevel { get; set; }
    public string? Semester { get; set; }
    public decimal? Units { get; set; }
    public string? ScheduleJson { get; set; }
    public bool IsActive { get; set; }
}