namespace CSHSBackendAPI.Application.Payments.DTOs;

public class PaymentDto
{
    public long Id { get; set; }
    public long EnrollmentId { get; set; }
    public long? StudentId { get; set; }
    public string? FeeBreakdownJson { get; set; }
    public string? DiscountsAppliedJson { get; set; }
    public decimal TotalFee { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal Balance { get; set; }
    public string? PaymentMethod { get; set; }
    public string? OrNumber { get; set; }
    public DateOnly PaymentDate { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}