using CSHSBackendAPI.Domain.Common;

namespace CSHSBackendAPI.Domain.Entities;

public class SchoolYear : BaseEntity
{
    public long SchoolId { get; set; }
    public string YearLabel { get; set; } = string.Empty;
    public DateOnly DateStart { get; set; }
    public DateOnly DateEnd { get; set; }
    public bool IsCurrent { get; set; } = false;
    public bool IsLocked { get; set; } = false;

    // Relationships
    public School School { get; set; } = null!;
}