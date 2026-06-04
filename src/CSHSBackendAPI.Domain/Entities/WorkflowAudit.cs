using CSHSBackendAPI.Domain.Common;

namespace CSHSBackendAPI.Domain.Entities;

public class WorkflowAudit : BaseEntity
{
    public string WorkflowId { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public long EntityId { get; set; }
    public string? FromStep { get; set; }
    public string ToStep { get; set; } = string.Empty;
    public string? ActionId { get; set; }
    public long? ByUserId { get; set; }
    public string ByName { get; set; } = string.Empty;
    public string ByRole { get; set; } = string.Empty;
    public string? Note { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
}