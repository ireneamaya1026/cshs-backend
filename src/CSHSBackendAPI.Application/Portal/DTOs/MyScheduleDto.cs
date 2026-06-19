namespace CSHSBackendAPI.Application.Portal.DTOs;

public class MyScheduleDto
{
    public string Subject { get; set; } = string.Empty;
    public string? TeacherName { get; set; }
    public string? ScheduleJson { get; set; }
}