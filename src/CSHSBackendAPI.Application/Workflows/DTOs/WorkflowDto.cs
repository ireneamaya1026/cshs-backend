namespace CSHSBackendAPI.Application.Workflows.DTOs;

public class WorkflowDto
{
    public long Id { get; set; }
    public string WorkflowId { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public short Version { get; set; }
    public string StepsJson { get; set; } = string.Empty;
    public string PermissionsJson { get; set; } = string.Empty;
    public bool IsLocked { get; set; }
}