namespace CSHSBackendAPI.Application.Documents.DTOs;

public class CreateDocumentRequest
{
    public long StudentId { get; set; }
    public long CampusId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string Department { get; set; } = "basicEd";
    public string DocumentType { get; set; } = string.Empty;
    public string DocumentLabel { get; set; } = string.Empty;
    public string? Purpose { get; set; }
    public string RequestedBy { get; set; } = "admin";
    public string? RequestorName { get; set; }
    public decimal Fee { get; set; } = 0;
    public bool RequiresClearance { get; set; } = false;
}