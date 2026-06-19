using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Payments.DTOs;

namespace CSHSBackendAPI.Application.Payments.Queries.GetPayments;

public class GetPaymentsQueryHandler
{
    private readonly IPaymentRepository _repo;

    public GetPaymentsQueryHandler(IPaymentRepository repo) =>
        _repo = repo;

    public async Task<IEnumerable<PaymentDto>> Handle(
        long? enrollmentId, long? studentId,
        DateOnly? dateFrom, DateOnly? dateTo)
    {
        var items = await _repo.GetAllAsync(enrollmentId, studentId, dateFrom, dateTo);

        return items.Select(p => new PaymentDto
        {
            Id = p.Id,
            EnrollmentId = p.EnrollmentId,
            StudentId = p.StudentId,
            FeeBreakdownJson = p.FeeBreakdownJson,
            DiscountsAppliedJson = p.DiscountsAppliedJson,
            TotalFee = p.TotalFee,
            AmountPaid = p.AmountPaid,
            Balance = p.Balance,
            PaymentMethod = p.PaymentMethod,
            OrNumber = p.OrNumber,
            PaymentDate = p.PaymentDate,
            Notes = p.Notes,
            CreatedAt = p.CreatedAt
        });
    }
}