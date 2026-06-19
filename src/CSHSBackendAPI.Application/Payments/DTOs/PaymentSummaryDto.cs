namespace CSHSBackendAPI.Application.Payments.DTOs;

public class PaymentSummaryDto
{
    public string SchoolYear { get; set; } = string.Empty;
    public long? CampusId { get; set; }
    public decimal TotalCollected { get; set; }
    public decimal TotalBalance { get; set; }
    public int TotalTransactions { get; set; }
}