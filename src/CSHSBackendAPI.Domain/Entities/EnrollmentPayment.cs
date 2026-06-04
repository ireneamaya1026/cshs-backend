using CSHSBackendAPI.Domain.Common;

namespace CSHSBackendAPI.Domain.Entities;

public class EnrollmentPayment : BaseEntity
{
    public long EnrollmentId { get; set; }
    public long? StudentId { get; set; }
    public long SchoolId { get; set; }
    public string? FeeBreakdownJson { get; set; }
    public string? DiscountsAppliedJson { get; set; }
    public decimal TotalFee { get; set; } = 0;
    public decimal AmountPaid { get; set; } = 0;
    public decimal Balance { get; set; } = 0;
    public string? PaymentMethod { get; set; }
    public string? OrNumber { get; set; }
    public DateOnly PaymentDate { get; set; }
    public string? Notes { get; set; }
    public long? RecordedById { get; set; }

    // Relationships
    public Enrollment Enrollment { get; set; } = null!;
    public Student? Student { get; set; }
     
    public SystemUser? RecordedBy { get; set; }
}