using CSHSBackendAPI.Domain.Common;

namespace CSHSBackendAPI.Domain.Entities;

public class FormTemplate : BaseEntity
{
    public string FormType { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string FieldsJson { get; set; } = "[]";
    public bool IsActive { get; set; } = true;
    public long? CreatedById { get; set; }

    public SystemUser? CreatedBy { get; set; }
}