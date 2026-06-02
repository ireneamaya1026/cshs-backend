using CSHSBackendAPI.Domain.Common;

namespace CSHSBackendAPI.Domain.Entities;

public class FormTemplate : BaseEntity
{
    public long SchoolId { get; set; }
    public string FormType { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string FieldsJson { get; set; } = "[]";
    public bool IsActive { get; set; } = true;
    public long? CreatedById { get; set; }

    // Relationships
    public School School { get; set; } = null!;
    public SystemUser? CreatedBy { get; set; }
}