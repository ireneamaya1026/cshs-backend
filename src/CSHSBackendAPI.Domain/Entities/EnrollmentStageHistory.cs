using CSHSBackendAPI.Domain.Common;

namespace CSHSBackendAPI.Domain.Entities;

public class EnrollmentStageHistory : BaseEntity
{
    public long EnrollmentId { get; set; }
    public string Step { get; set; } = string.Empty;
    public string? FromStep { get; set; }
    public string? ActionId { get; set; }
    public long? ByUserId { get; set; }
    public string ByName { get; set; } = string.Empty;
    public string ByRole { get; set; } = string.Empty;
    public string? Note { get; set; }
    public string? ExtraDataJson { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    // Relationships
    public Enrollment Enrollment { get; set; } = null!;
}