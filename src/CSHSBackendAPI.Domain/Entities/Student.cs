using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class Student : BaseEntity
{
    public long SchoolId { get; set; }
    public long CampusId { get; set; }
    public string StudentId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? Suffix { get; set; }
    public DateOnly? Birthdate { get; set; }
    public Sex? Sex { get; set; }
    public string? ContactNumber { get; set; }
    public string? Address { get; set; }
    public string GradeLevel { get; set; } = string.Empty;
    public string? Section { get; set; }
    public Department Department { get; set; } = Department.BasicEd;
    public string? Program { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string? Lrn { get; set; }
    public decimal Balance { get; set; } = 0;
    public string? PortalPasswordHash { get; set; }
    public StudentStatus Status { get; set; } = StudentStatus.Active;
    public long? ConvertedFromEnrollmentId { get; set; }

    // Relationships
    public School School { get; set; } = null!;
    public Campus Campus { get; set; } = null!;
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<BasicEdGrade> BasicEdGrades { get; set; } = new List<BasicEdGrade>();
    public ICollection<CollegeGrade> CollegeGrades { get; set; } = new List<CollegeGrade>();
    public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
}