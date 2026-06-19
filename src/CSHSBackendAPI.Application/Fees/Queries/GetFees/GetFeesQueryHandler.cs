using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Fees.DTOs;

namespace CSHSBackendAPI.Application.Fees.Queries.GetFees;

public class GetFeesQueryHandler
{
    private readonly IFeeRepository _repo;

    public GetFeesQueryHandler(IFeeRepository repo) =>
        _repo = repo;

    public async Task<IEnumerable<FeeStructureDto>> Handle(string? schoolYear, string? gradeLevel)
    {
        var fees = await _repo.GetAllAsync(schoolYear, gradeLevel);

        return fees.Select(f => new FeeStructureDto
        {
            Id = f.Id,
            CampusId = f.CampusId,
            SchoolYear = f.SchoolYear,
            GradeLevel = f.GradeLevel,
            StudentType = f.StudentType?.ToString(),
            Tuition = f.Tuition,
            Misc = f.Misc,
            Lab = f.Lab,
            Books = f.Books,
            Other = f.Other,
            EnrollmentFee = f.EnrollmentFee,
            TotalFee = f.TotalFee,
            IsActive = f.IsActive
        });
    }
}