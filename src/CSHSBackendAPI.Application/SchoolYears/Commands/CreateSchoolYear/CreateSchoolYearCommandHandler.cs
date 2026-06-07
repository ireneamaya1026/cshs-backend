using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.SchoolYears.DTOs;
using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.SchoolYears.Commands.CreateSchoolYear;

public class CreateSchoolYearCommandHandler
{
    private readonly ISchoolYearRepository _schoolYearRepo;

    public CreateSchoolYearCommandHandler(ISchoolYearRepository schoolYearRepo) =>
        _schoolYearRepo = schoolYearRepo;

    public async Task<SchoolYearDto> Handle(CreateSchoolYearRequest request)
    {
        // Check year label is unique
        if (await _schoolYearRepo.YearLabelExistsAsync(request.YearLabel))
            throw new ValidationException(new List<string>
            {
                $"School year '{request.YearLabel}' already exists."
            });

        // Validate date range
        if (request.DateEnd <= request.DateStart)
            throw new ValidationException(new List<string>
            {
                "End date must be after start date."
            });

        var schoolYear = new SchoolYear
        {
            YearLabel = request.YearLabel,
            DateStart = request.DateStart,
            DateEnd = request.DateEnd,
            IsCurrent = false,
            IsLocked = false
        };

        var created = await _schoolYearRepo.CreateAsync(schoolYear);

        return new SchoolYearDto
        {
            Id = created.Id,
            YearLabel = created.YearLabel,
            DateStart = created.DateStart,
            DateEnd = created.DateEnd,
            IsCurrent = created.IsCurrent,
            IsLocked = created.IsLocked
        };
    }
}