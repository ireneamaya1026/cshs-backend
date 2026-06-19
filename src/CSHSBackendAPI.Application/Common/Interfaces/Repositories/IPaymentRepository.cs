using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<IEnumerable<EnrollmentPayment>> GetAllAsync(
        long? enrollmentId, long? studentId,
        DateOnly? dateFrom, DateOnly? dateTo);
    Task<PaymentSummary> GetSummaryAsync(string schoolYear, long? campusId);
    Task<EnrollmentPayment> CreateAsync(EnrollmentPayment payment);
}

public class PaymentSummary
{
    public decimal TotalCollected { get; set; }
    public decimal TotalBalance { get; set; }
    public int TotalTransactions { get; set; }
}