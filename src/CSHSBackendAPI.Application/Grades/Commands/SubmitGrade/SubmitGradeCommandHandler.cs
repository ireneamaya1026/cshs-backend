using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Grades.Commands.SubmitGrade;

public class SubmitGradeCommandHandler
{
    private readonly IGradeRepository _gradeRepo;

    public SubmitGradeCommandHandler(IGradeRepository gradeRepo) =>
        _gradeRepo = gradeRepo;

    public async Task HandleBasicEd(long id)
    {
        var grade = await _gradeRepo.GetBasicEdGradeByIdAsync(id)
            ?? throw new NotFoundException("BasicEdGrade", id);

        if (grade.Status != GradeStatus.Draft)
            throw new ValidationException(new List<string>
            {
                "Only draft grades can be submitted."
            });

        grade.Status = GradeStatus.Submitted;
        grade.SubmittedAt = DateTime.UtcNow;

        await _gradeRepo.UpdateBasicEdGradeAsync(grade);
    }

    public async Task HandleCollege(long id)
    {
        var grade = await _gradeRepo.GetCollegeGradeByIdAsync(id)
            ?? throw new NotFoundException("CollegeGrade", id);

        if (grade.Status != GradeStatus.Draft)
            throw new ValidationException(new List<string>
            {
                "Only draft grades can be submitted."
            });

        grade.Status = GradeStatus.Submitted;
        grade.SubmittedAt = DateTime.UtcNow;

        await _gradeRepo.UpdateCollegeGradeAsync(grade);
    }
}