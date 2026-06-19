using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Grades.DTOs;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Grades.Commands.CreateActivity;

public class CreateActivityCommandHandler
{
    private readonly IGradeRepository _gradeRepo;

    public CreateActivityCommandHandler(IGradeRepository gradeRepo) =>
        _gradeRepo = gradeRepo;

    public async Task<ActivityDto> Handle(CreateActivityRequest request)
    {
        if (!Enum.TryParse<GradePeriod>(request.Period, true, out var period))
            throw new ValidationException(new List<string> { $"Invalid period '{request.Period}'." });

        if (!Enum.TryParse<ActivityCategory>(request.Category, true, out var category))
            throw new ValidationException(new List<string> { $"Invalid category '{request.Category}'." });

        var activity = new GradeActivity
        {
            SubjectLoadId = request.SubjectLoadId,
            Period = period,
            Category = category,
            Title = request.Title,
            MaxScore = request.MaxScore,
            Weight = request.Weight,
            SortOrder = request.SortOrder,
            IsActive = true
        };

        var created = await _gradeRepo.CreateActivityAsync(activity);

        return new ActivityDto
        {
            Id = created.Id,
            SubjectLoadId = created.SubjectLoadId,
            Period = created.Period.ToString(),
            Category = created.Category.ToString(),
            Title = created.Title,
            MaxScore = created.MaxScore,
            Weight = created.Weight,
            SortOrder = created.SortOrder,
            IsActive = created.IsActive
        };
    }
}