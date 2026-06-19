namespace CSHSBackendAPI.Application.Documents.DTOs;

public class DocumentRequestDto
{
    public long Id { get; set; }
    public long CampusId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public long StudentId { get; set; }
    public string Department { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty;
    public string DocumentLabel { get; set; } = string.Empty;
    public string? Purpose { get; set; }
    public string RequestedBy { get; set; } = string.Empty;
    public string? RequestorName { get; set; }
    public decimal Fee { get; set; }
    public DateTime? PaidAt { get; set; }
    public bool RequiresClearance { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? ReleasedTo { get; set; }
    public string? ClaimSlip { get; set; }
    public DateTime? ReleasedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}