namespace CSHSBackendAPI.Application.FormTemplates.DTOs;

public class FormTemplateDto
{
    public long Id { get; set; }
    public string FormType { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string FieldsJson { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}