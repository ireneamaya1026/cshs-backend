namespace CSHSBackendAPI.Application.Workflows.DTOs;

public class UpdateWorkflowRequest
{
    public string? StepsJson { get; set; }
    public string? PermissionsJson { get; set; }
}