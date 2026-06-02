using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class WorkflowDefinition : BaseEntity
{
    public long SchoolId { get; set; }
    public string WorkflowId { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public Department Department { get; set; } = Department.BasicEd;
    public short Version { get; set; } = 1;
    public string StepsJson { get; set; } = "[]";
    public string PermissionsJson { get; set; } = "{}";
    public bool IsLocked { get; set; } = false;
    public long? CreatedById { get; set; }

    // Relationships
    public School School { get; set; } = null!;
}