using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Payments.DTOs;

namespace CSHSBackendAPI.Application.Payments.Queries.GetPaymentSummary;

public class GetPaymentSummaryQueryHandler
{
    private readonly IPaymentRepository _repo;

    public GetPaymentSummaryQueryHandler(IPaymentRepository repo) =>
        _repo = repo;

    public async Task<PaymentSummaryDto> Handle(string schoolYear, long? campusId)
    {
        var summary = await _repo.GetSummaryAsync(schoolYear, campusId);

        return new PaymentSummaryDto
        {
            SchoolYear = schoolYear,
            CampusId = campusId,
            TotalCollected = summary.TotalCollected,
            TotalBalance = summary.TotalBalance,
            TotalTransactions = summary.TotalTransactions
        };
    }
}