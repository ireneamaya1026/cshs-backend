using CSHSBackendAPI.Domain.Common;

namespace CSHSBackendAPI.Domain.Entities;

public class ClearanceDeptSignoff : BaseEntity
{
    public long ClearanceId { get; set; }
    public string DeptId { get; set; } = string.Empty;
    public string DeptLabel { get; set; } = string.Empty;
    public bool IsCleared { get; set; } = false;
    public long? ClearedById { get; set; }
    public string? ClearedByName { get; set; }
    public DateTime? ClearedAt { get; set; }
    public bool IsAutoCleared { get; set; } = false;

    // Relationships
    public Clearance Clearance { get; set; } = null!;
    public SystemUser? ClearedBy { get; set; }
}