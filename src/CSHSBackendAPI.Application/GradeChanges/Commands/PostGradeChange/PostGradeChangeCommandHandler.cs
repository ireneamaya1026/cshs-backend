using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.GradeChanges.Commands.PostGradeChange;

public class PostGradeChangeCommandHandler
{
    private readonly IGradeChangeRepository _gradeChangeRepo;
    private readonly IGradeRepository _gradeRepo;

    public PostGradeChangeCommandHandler(
        IGradeChangeRepository gradeChangeRepo,
        IGradeRepository gradeRepo)
    {
        _gradeChangeRepo = gradeChangeRepo;
        _gradeRepo = gradeRepo;
    }

    public async Task Handle(long id, long postedById, string postedByName, string postedByRole)
    {
        var gradeChange = await _gradeChangeRepo.GetByIdAsync(id)
            ?? throw new NotFoundException("GradeChangeRequest", id);

        if (gradeChange.Status != GradeChangeStatus.Approved)
            throw new ValidationException(new List<string>
            {
                "Only approved grade changes can be posted."
            });

        // Apply grade change to actual grade record
        if (gradeChange.GradeType == "basicEd" && gradeChange.BasicGradeId.HasValue)
        {
            var grade = await _gradeRepo.GetBasicEdGradeByIdAsync(gradeChange.BasicGradeId.Value);
            if (grade != null)
            {
                grade.Transmuted = gradeChange.RequestedGrade;
                await _gradeRepo.UpdateBasicEdGradeAsync(grade);
            }
        }
        else if (gradeChange.GradeType == "college" && gradeChange.CollegeGradeId.HasValue)
        {
            var grade = await _gradeRepo.GetCollegeGradeByIdAsync(gradeChange.CollegeGradeId.Value);
            if (grade != null)
            {
                grade.SemesterGrade = gradeChange.RequestedGrade;
                await _gradeRepo.UpdateCollegeGradeAsync(grade);
            }
        }

        gradeChange.Status = GradeChangeStatus.Posted;
        gradeChange.PostedById = postedById;
        gradeChange.PostedAt = DateTime.UtcNow;

        await _gradeChangeRepo.UpdateAsync(gradeChange);

        await _gradeChangeRepo.AddAuditAsync(new GradeChangeAudit
        {
            RequestId = id,
            Action = "posted",
            ByUserId = postedById,
            ByName = postedByName,
            ByRole = postedByRole,
            RecordedAt = DateTime.UtcNow
        });
    }
}