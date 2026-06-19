namespace CSHSBackendAPI.Application.Payments.DTOs;

public class RecordPaymentRequest
{
    public long EnrollmentId { get; set; }
    public decimal AmountPaid { get; set; }
    public string? PaymentMethod { get; set; }
    public string? OrNumber { get; set; }
    public string? FeeBreakdownJson { get; set; }
    public DateOnly PaymentDate { get; set; }
    public string? Notes { get; set; }
}