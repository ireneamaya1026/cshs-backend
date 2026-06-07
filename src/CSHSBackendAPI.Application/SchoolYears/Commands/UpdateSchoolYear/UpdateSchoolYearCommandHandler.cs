using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.SchoolYears.DTOs;

namespace CSHSBackendAPI.Application.SchoolYears.Commands.UpdateSchoolYear;

public class UpdateSchoolYearCommandHandler
{
    private readonly ISchoolYearRepository _schoolYearRepo;

    public UpdateSchoolYearCommandHandler(ISchoolYearRepository schoolYearRepo) =>
        _schoolYearRepo = schoolYearRepo;

    public async Task<SchoolYearDto> Handle(long id, UpdateSchoolYearRequest request)
    {
        var schoolYear = await _schoolYearRepo.GetByIdAsync(id)
            ?? throw new NotFoundException("SchoolYear", id);

        // Cannot modify a locked year
        if (schoolYear.IsLocked && request.IsLocked != true)
            throw new ValidationException(new List<string>
            {
                "This school year is locked and cannot be modified."
            });

        // If setting as current, unset all others first
        if (request.IsCurrent == true)
            await _schoolYearRepo.UnsetCurrentAsync();

        if (request.IsCurrent.HasValue) schoolYear.IsCurrent = request.IsCurrent.Value;
        if (request.IsLocked.HasValue) schoolYear.IsLocked = request.IsLocked.Value;

        await _schoolYearRepo.UpdateAsync(schoolYear);

        return new SchoolYearDto
        {
            Id = schoolYear.Id,
            YearLabel = schoolYear.YearLabel,
            DateStart = schoolYear.DateStart,
            DateEnd = schoolYear.DateEnd,
            IsCurrent = schoolYear.IsCurrent,
            IsLocked = schoolYear.IsLocked
        };
    }
}