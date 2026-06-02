using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class AttendanceRecord : BaseEntity
{
    public long SchoolId { get; set; }
    public long CampusId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public long StudentId { get; set; }
    public long? SubjectLoadId { get; set; }
    public long TeacherId { get; set; }
    public string? Section { get; set; }
    public DateOnly Date { get; set; }
    public string DayOfWeek { get; set; } = string.Empty;
    public AttendanceStatus Status { get; set; } = AttendanceStatus.Present;
    public string? Remarks { get; set; }
    public decimal AbsenceEquivalent { get; set; } = 0;

    // Relationships
    public School School { get; set; } = null!;
    public Campus Campus { get; set; } = null!;
    public Student Student { get; set; } = null!;
    public SubjectLoad? SubjectLoad { get; set; }
    public SystemUser Teacher { get; set; } = null!;
}