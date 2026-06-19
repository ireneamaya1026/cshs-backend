namespace CSHSBackendAPI.Application.Documents.DTOs;

public class ReleaseDocumentRequest
{
    public string ReleasedTo { get; set; } = string.Empty;
    public string? ClaimSlip { get; set; }
}