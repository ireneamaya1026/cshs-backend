using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Grades.DTOs;
using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Grades.Commands.SaveScores;

public class SaveScoresCommandHandler
{
    private readonly IGradeRepository _gradeRepo;

    public SaveScoresCommandHandler(IGradeRepository gradeRepo) =>
        _gradeRepo = gradeRepo;

    public async Task Handle(SaveScoresRequest request, long recordedById)
    {
        foreach (var item in request.Scores)
        {
            var existing = await _gradeRepo.GetScoreAsync(request.ActivityId, item.StudentId);

            if (existing != null)
            {
                existing.Score = item.Score;
                existing.Remarks = item.Remarks;
                await _gradeRepo.UpdateScoreAsync(existing);
            }
            else
            {
                await _gradeRepo.CreateScoreAsync(new StudentActivityScore
                {
                    ActivityId = request.ActivityId,
                    StudentId = item.StudentId,
                    Score = item.Score,
                    Remarks = item.Remarks,
                    RecordedById = recordedById,
                    RecordedAt = DateTime.UtcNow
                });
            }
        }
    }
}