namespace CSHSBackendAPI.Application.Attendance.DTOs;

public class BatchAttendanceRequest
{
    public List<AttendanceRecordItem> Records { get; set; } = new();
}

public class AttendanceRecordItem
{
    public long StudentId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public long? SubjectLoadId { get; set; }
    public string? Section { get; set; }
    public string? Remarks { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public long CampusId { get; set; }
    public long TeacherId { get; set; }
}