using CSHSBackendAPI.Domain.Common;

namespace CSHSBackendAPI.Domain.Entities;

public class StudentActivityScore : BaseEntity
{
    public long ActivityId { get; set; }
    public long StudentId { get; set; }
    public decimal? Score { get; set; }
    public string? Remarks { get; set; }
    public long? RecordedById { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    // Relationships
    public GradeActivity Activity { get; set; } = null!;
    public Student Student { get; set; } = null!;
    public SystemUser? RecordedBy { get; set; }
}