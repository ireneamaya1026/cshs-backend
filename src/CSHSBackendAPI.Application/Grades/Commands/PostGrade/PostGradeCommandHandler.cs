using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Grades.Commands.PostGrade;

public class PostGradeCommandHandler
{
    private readonly IGradeRepository _gradeRepo;

    public PostGradeCommandHandler(IGradeRepository gradeRepo) =>
        _gradeRepo = gradeRepo;

    public async Task HandleBasicEd(long id)
    {
        var grade = await _gradeRepo.GetBasicEdGradeByIdAsync(id)
            ?? throw new NotFoundException("BasicEdGrade", id);

        if (grade.Status != GradeStatus.Approved)
            throw new ValidationException(new List<string>
            {
                "Only approved grades can be posted."
            });

        grade.Status = GradeStatus.Posted;
        await _gradeRepo.UpdateBasicEdGradeAsync(grade);
    }

    public async Task HandleCollege(long id)
    {
        var grade = await _gradeRepo.GetCollegeGradeByIdAsync(id)
            ?? throw new NotFoundException("CollegeGrade", id);

        if (grade.Status != GradeStatus.Approved)
            throw new ValidationException(new List<string>
            {
                "Only approved grades can be posted."
            });

        grade.Status = GradeStatus.Posted;
        await _gradeRepo.UpdateCollegeGradeAsync(grade);
    }
}