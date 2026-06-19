using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Fees.DTOs;

namespace CSHSBackendAPI.Application.Fees.Commands.UpdateFee;

public class UpdateFeeCommandHandler
{
    private readonly IFeeRepository _repo;

    public UpdateFeeCommandHandler(IFeeRepository repo) =>
        _repo = repo;

    public async Task<FeeStructureDto> Handle(long id, UpdateFeeRequest request)
    {
        var fee = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException("FeeStructure", id);

        if (request.Tuition.HasValue) fee.Tuition = request.Tuition.Value;
        if (request.Misc.HasValue) fee.Misc = request.Misc.Value;
        if (request.Lab.HasValue) fee.Lab = request.Lab.Value;
        if (request.Books.HasValue) fee.Books = request.Books.Value;
        if (request.Other.HasValue) fee.Other = request.Other.Value;
        if (request.EnrollmentFee.HasValue) fee.EnrollmentFee = request.EnrollmentFee.Value;

        fee.TotalFee = fee.Tuition + fee.Misc + fee.Lab +
                       fee.Books + fee.Other + fee.EnrollmentFee;

        await _repo.UpdateAsync(fee);

        return new FeeStructureDto
        {
            Id = fee.Id,
            SchoolYear = fee.SchoolYear,
            GradeLevel = fee.GradeLevel,
            Tuition = fee.Tuition,
            Misc = fee.Misc,
            Lab = fee.Lab,
            Books = fee.Books,
            Other = fee.Other,
            EnrollmentFee = fee.EnrollmentFee,
            TotalFee = fee.TotalFee,
            IsActive = fee.IsActive
        };
    }
}