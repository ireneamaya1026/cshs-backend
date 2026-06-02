using CSHSBackendAPI.Domain.Common;

namespace CSHSBackendAPI.Domain.Entities;

public class GradeChangeAudit : BaseEntity
{
    public long RequestId { get; set; }
    public string Action { get; set; } = string.Empty;
    public long? ByUserId { get; set; }
    public string ByName { get; set; } = string.Empty;
    public string ByRole { get; set; } = string.Empty;
    public string? Note { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    // Relationships
    public GradeChangeRequest Request { get; set; } = null!;
}