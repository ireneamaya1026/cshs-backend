namespace CSHSBackendAPI.Application.Documents.DTOs;

public class AdvanceDocumentRequest
{
    public string ActionId { get; set; } = string.Empty;
    public string? Note { get; set; }
}