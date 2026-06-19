namespace CSHSBackendAPI.Application.SubjectLoads.DTOs;

public class UpdateSubjectLoadRequest
{
    public string? GradeLevel { get; set; }
    public string? Section { get; set; }
    public string? SubjectArea { get; set; }
    public string? Subject { get; set; }
    public string? Program { get; set; }
    public string? YearLevel { get; set; }
    public string? Semester { get; set; }
    public decimal? Units { get; set; }
    public string? ScheduleJson { get; set; }
    public string? TeacherName { get; set; }
}