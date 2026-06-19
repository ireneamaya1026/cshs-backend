namespace CSHSBackendAPI.Application.FormTemplates.DTOs;

public class UpsertFormTemplateRequest
{
    public string FormType { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string FieldsJson { get; set; } = string.Empty;
}