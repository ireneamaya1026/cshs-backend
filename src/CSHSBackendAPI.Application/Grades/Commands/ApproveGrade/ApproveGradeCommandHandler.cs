using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Grades.DTOs;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Grades.Commands.ApproveGrade;

public class ApproveGradeCommandHandler
{
    private readonly IGradeRepository _gradeRepo;

    public ApproveGradeCommandHandler(IGradeRepository gradeRepo) =>
        _gradeRepo = gradeRepo;

    public async Task HandleBasicEd(long id, long approvedById)
    {
        var grade = await _gradeRepo.GetBasicEdGradeByIdAsync(id)
            ?? throw new NotFoundException("BasicEdGrade", id);

        if (grade.Status != GradeStatus.Submitted)
            throw new ValidationException(new List<string>
            {
                "Only submitted grades can be approved."
            });

        grade.Status = GradeStatus.Approved;
        grade.ApprovedAt = DateTime.UtcNow;
        grade.ApprovedById = approvedById;

        await _gradeRepo.UpdateBasicEdGradeAsync(grade);
    }

    public async Task HandleCollege(long id, long approvedById)
    {
        var grade = await _gradeRepo.GetCollegeGradeByIdAsync(id)
            ?? throw new NotFoundException("CollegeGrade", id);

        if (grade.Status != GradeStatus.Submitted)
            throw new ValidationException(new List<string>
            {
                "Only submitted grades can be approved."
            });

        grade.Status = GradeStatus.Approved;
        grade.ApprovedAt = DateTime.UtcNow;
        grade.ApprovedById = approvedById;

        await _gradeRepo.UpdateCollegeGradeAsync(grade);
    }
}