using CSHSBackendAPI.Domain.Common;

namespace CSHSBackendAPI.Domain.Entities;

public class Campus : BaseEntity
{
    public long SchoolId { get; set; }
    public string CampusKey { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? ShortName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; } = true;
    public bool HasBasicEd { get; set; } = true;
    public bool HasCollege { get; set; } = false;
    public string? CollegeProgramsJson { get; set; }
    public short SortOrder { get; set; } = 0;

    // Relationships
    public School School { get; set; } = null!;
    public ICollection<SystemUser> SystemUsers { get; set; } = new List<SystemUser>();
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}