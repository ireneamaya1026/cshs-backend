using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Payments.DTOs;
using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Payments.Commands.RecordPayment;

public class RecordPaymentCommandHandler
{
    private readonly IPaymentRepository _paymentRepo;
    private readonly IEnrollmentRepository _enrollmentRepo;

    public RecordPaymentCommandHandler(
        IPaymentRepository paymentRepo,
        IEnrollmentRepository enrollmentRepo)
    {
        _paymentRepo = paymentRepo;
        _enrollmentRepo = enrollmentRepo;
    }

    public async Task<PaymentDto> Handle(RecordPaymentRequest request, long recordedById)
    {
        var enrollment = await _enrollmentRepo.GetByIdAsync(request.EnrollmentId)
            ?? throw new NotFoundException("Enrollment", request.EnrollmentId);

        var newBalance = enrollment.Balance - request.AmountPaid;

        var payment = new EnrollmentPayment
        {
            EnrollmentId = request.EnrollmentId,
            StudentId = enrollment.StudentId,
            FeeBreakdownJson = request.FeeBreakdownJson,
            TotalFee = enrollment.NetFee,
            AmountPaid = request.AmountPaid,
            Balance = newBalance,
            PaymentMethod = request.PaymentMethod,
            OrNumber = request.OrNumber,
            PaymentDate = request.PaymentDate,
            Notes = request.Notes,
            RecordedById = recordedById
        };

        var created = await _paymentRepo.CreateAsync(payment);

        // Update enrollment balance
        enrollment.AmountPaid += request.AmountPaid;
        enrollment.Balance = newBalance;
        await _enrollmentRepo.UpdateAsync(enrollment);

        return new PaymentDto
        {
            Id = created.Id,
            EnrollmentId = created.EnrollmentId,
            AmountPaid = created.AmountPaid,
            Balance = created.Balance,
            OrNumber = created.OrNumber,
            PaymentDate = created.PaymentDate,
            CreatedAt = created.CreatedAt
        };
    }
}