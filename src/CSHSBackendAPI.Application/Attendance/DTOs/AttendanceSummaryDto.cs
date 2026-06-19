namespace CSHSBackendAPI.Application.Attendance.DTOs;

public class AttendanceSummaryDto
{
    public long StudentId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public int TotalDays { get; set; }
    public int Present { get; set; }
    public int Absent { get; set; }
    public int Late { get; set; }
    public int Excused { get; set; }
    public decimal TotalAbsenceEquivalent { get; set; }
}