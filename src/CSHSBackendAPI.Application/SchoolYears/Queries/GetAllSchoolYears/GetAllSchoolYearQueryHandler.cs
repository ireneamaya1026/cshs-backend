using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.SchoolYears.DTOs;

namespace CSHSBackendAPI.Application.SchoolYears.Queries.GetAllSchoolYears;

public class GetAllSchoolYearsQueryHandler
{
    private readonly ISchoolYearRepository _schoolYearRepo;

    public GetAllSchoolYearsQueryHandler(ISchoolYearRepository schoolYearRepo) =>
        _schoolYearRepo = schoolYearRepo;

    public async Task<IEnumerable<SchoolYearDto>> Handle()
    {
        var schoolYears = await _schoolYearRepo.GetAllAsync();

        return schoolYears.Select(s => new SchoolYearDto
        {
            Id = s.Id,
            YearLabel = s.YearLabel,
            DateStart = s.DateStart,
            DateEnd = s.DateEnd,
            IsCurrent = s.IsCurrent,
            IsLocked = s.IsLocked
        });
    }
}