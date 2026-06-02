using CSHSBackendAPI.Domain.Common;

namespace CSHSBackendAPI.Domain.Entities;

public class DocumentStatusHistory : BaseEntity
{
    public long RequestId { get; set; }
    public string Status { get; set; } = string.Empty;
    public long? ByUserId { get; set; }
    public string ByName { get; set; } = string.Empty;
    public string? Note { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    // Relationships
    public DocumentRequest Request { get; set; } = null!;
}