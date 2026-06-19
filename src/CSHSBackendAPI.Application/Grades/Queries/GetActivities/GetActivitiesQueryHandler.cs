using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Grades.DTOs;

namespace CSHSBackendAPI.Application.Grades.Queries.GetActivities;

public class GetActivitiesQueryHandler
{
    private readonly IGradeRepository _gradeRepo;

    public GetActivitiesQueryHandler(IGradeRepository gradeRepo) =>
        _gradeRepo = gradeRepo;

    public async Task<IEnumerable<ActivityDto>> Handle(long subjectLoadId, string? period)
    {
        var activities = await _gradeRepo.GetActivitiesAsync(subjectLoadId, period);

        return activities.Select(a => new ActivityDto
        {
            Id = a.Id,
            SubjectLoadId = a.SubjectLoadId,
            Period = a.Period.ToString(),
            Category = a.Category.ToString(),
            Title = a.Title,
            MaxScore = a.MaxScore,
            Weight = a.Weight,
            SortOrder = a.SortOrder,
            IsActive = a.IsActive
        });
    }
}