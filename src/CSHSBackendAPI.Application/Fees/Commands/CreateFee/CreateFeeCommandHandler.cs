using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Fees.DTOs;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Fees.Commands.CreateFee;

public class CreateFeeCommandHandler
{
    private readonly IFeeRepository _repo;

    public CreateFeeCommandHandler(IFeeRepository repo) =>
        _repo = repo;

    public async Task<FeeStructureDto> Handle(CreateFeeRequest request)
    {
        var totalFee = request.Tuition + request.Misc + request.Lab +
                       request.Books + request.Other + request.EnrollmentFee;

        StudentType? studentType = null;
        if (!string.IsNullOrEmpty(request.StudentType) &&
            Enum.TryParse<StudentType>(request.StudentType, true, out var st))
            studentType = st;

        var fee = new FeeStructure
        {
            CampusId = request.CampusId,
            SchoolYear = request.SchoolYear,
            GradeLevel = request.GradeLevel,
            StudentType = studentType,
            Tuition = request.Tuition,
            Misc = request.Misc,
            Lab = request.Lab,
            Books = request.Books,
            Other = request.Other,
            EnrollmentFee = request.EnrollmentFee,
            TotalFee = totalFee,
            IsActive = true
        };

        var created = await _repo.CreateAsync(fee);

        return new FeeStructureDto
        {
            Id = created.Id,
            SchoolYear = created.SchoolYear,
            GradeLevel = created.GradeLevel,
            Tuition = created.Tuition,
            TotalFee = created.TotalFee,
            IsActive = created.IsActive
        };
    }
}