using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class SubjectLoad : BaseEntity
{
    public long SchoolId { get; set; }
    public long CampusId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public Department Department { get; set; }
    public long TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    // Basic Ed
    public string? GradeLevel { get; set; }
    public string? Section { get; set; }
    public string? SubjectArea { get; set; }
    public string Subject { get; set; } = string.Empty;
    // College
    public string? Program { get; set; }
    public string? YearLevel { get; set; }
    public string? Semester { get; set; }
    public decimal? Units { get; set; }
    public string? ScheduleJson { get; set; }
    public bool IsActive { get; set; } = true;

    // Relationships
    public School School { get; set; } = null!;
    public Campus Campus { get; set; } = null!;
    public SystemUser Teacher { get; set; } = null!;
    public ICollection<BasicEdGrade> BasicEdGrades { get; set; } = new List<BasicEdGrade>();
    public ICollection<CollegeGrade> CollegeGrades { get; set; } = new List<CollegeGrade>();
    public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
    public ICollection<GradeActivity> GradeActivities { get; set; } = new List<GradeActivity>();
}