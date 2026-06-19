namespace CSHSBackendAPI.Application.Workflows.DTOs;

public class LogWorkflowAuditRequest
{
    public string WorkflowId { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public long EntityId { get; set; }
    public string? FromStep { get; set; }
    public string ToStep { get; set; } = string.Empty;
    public string? ActionId { get; set; }
    public string? Note { get; set; }
}