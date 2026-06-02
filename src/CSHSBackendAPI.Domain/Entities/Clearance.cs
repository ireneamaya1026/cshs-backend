using CSHSBackendAPI.Domain.Common;

namespace CSHSBackendAPI.Domain.Entities;

public class Clearance : BaseEntity
{
    public long SchoolId { get; set; }
    public long CampusId { get; set; }
    public long StudentId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string? Reason { get; set; }
    public bool HasUnpaidBalance { get; set; } = false;
    public bool IsFullyCleared { get; set; } = false;
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    // Relationships
    public School School { get; set; } = null!;
    public Campus Campus { get; set; } = null!;
    public Student Student { get; set; } = null!;
    public ICollection<ClearanceDeptSignoff> DeptSignoffs { get; set; } = new List<ClearanceDeptSignoff>();
}