namespace CSHSBackendAPI.Application.Students.DTOs;

public class StudentAttendanceDto
{
    public long Id { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public string DayOfWeek { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? Remarks { get; set; }
    public decimal AbsenceEquivalent { get; set; }
    public string? Section { get; set; }
}