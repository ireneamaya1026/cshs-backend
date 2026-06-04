using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class DocumentRequest : BaseEntity
{
    public long SchoolId { get; set; }
    public long CampusId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public long StudentId { get; set; }
    public Department Department { get; set; } = Department.BasicEd;
    public string DocumentType { get; set; } = string.Empty;
    public string DocumentLabel { get; set; } = string.Empty;
    public string? Purpose { get; set; }
    public string RequestedBy { get; set; } = string.Empty;
    public string? RequestorName { get; set; }
    public decimal Fee { get; set; } = 0;
    public DateTime? PaidAt { get; set; }
    public bool RequiresClearance { get; set; } = false;
    public long? ClearanceId { get; set; }
    public DocumentRequestStatus Status { get; set; } = DocumentRequestStatus.Requested;
    public string? ReleasedTo { get; set; }
    public string? ClaimSlip { get; set; }
    public DateTime? ReleasedAt { get; set; }
    public long? ReleasedById { get; set; }

    // Relationships
     
    public Campus Campus { get; set; } = null!;
    public Student Student { get; set; } = null!;
    public SystemUser? ReleasedBy { get; set; }
    public ICollection<DocumentStatusHistory> StatusHistories { get; set; } = new List<DocumentStatusHistory>();
}