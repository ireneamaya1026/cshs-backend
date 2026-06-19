using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<EnrollmentPayment>> GetAllAsync(
        long? enrollmentId, long? studentId,
        DateOnly? dateFrom, DateOnly? dateTo)
    {
        var query = _context.EnrollmentPayments.AsQueryable();

        if (enrollmentId.HasValue)
            query = query.Where(p => p.EnrollmentId == enrollmentId);

        if (studentId.HasValue)
            query = query.Where(p => p.StudentId == studentId);

        if (dateFrom.HasValue)
            query = query.Where(p => p.PaymentDate >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(p => p.PaymentDate <= dateTo.Value);

        return await query.OrderByDescending(p => p.PaymentDate).ToListAsync();
    }

    public async Task<PaymentSummary> GetSummaryAsync(string schoolYear, long? campusId)
    {
        var query = _context.EnrollmentPayments
            .Join(_context.Enrollments,
                p => p.EnrollmentId,
                e => e.Id,
                (p, e) => new { Payment = p, Enrollment = e })
            .Where(x => x.Enrollment.SchoolYear == schoolYear);

        if (campusId.HasValue)
            query = query.Where(x => x.Enrollment.CampusId == campusId);

        var payments = await query.Select(x => x.Payment).ToListAsync();

        return new PaymentSummary
        {
            TotalCollected = payments.Sum(p => p.AmountPaid),
            TotalBalance = payments.Sum(p => p.Balance),
            TotalTransactions = payments.Count
        };
    }

    public async Task<EnrollmentPayment> CreateAsync(EnrollmentPayment payment)
    {
        _context.EnrollmentPayments.Add(payment);
        await _context.SaveChangesAsync();
        return payment;
    }
}