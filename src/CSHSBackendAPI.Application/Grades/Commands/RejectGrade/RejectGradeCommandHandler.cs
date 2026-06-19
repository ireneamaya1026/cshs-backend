using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Grades.DTOs;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Grades.Commands.RejectGrade;

public class RejectGradeCommandHandler
{
    private readonly IGradeRepository _gradeRepo;

    public RejectGradeCommandHandler(IGradeRepository gradeRepo) =>
        _gradeRepo = gradeRepo;

    public async Task HandleBasicEd(long id, GradeActionRequest request)
    {
        var grade = await _gradeRepo.GetBasicEdGradeByIdAsync(id)
            ?? throw new NotFoundException("BasicEdGrade", id);

        if (grade.Status != GradeStatus.Submitted)
            throw new ValidationException(new List<string>
            {
                "Only submitted grades can be rejected."
            });

        grade.Status = GradeStatus.Rejected;
        grade.RejectionNote = request.Note;

        await _gradeRepo.UpdateBasicEdGradeAsync(grade);
    }

    public async Task HandleCollege(long id, GradeActionRequest request)
    {
        var grade = await _gradeRepo.GetCollegeGradeByIdAsync(id)
            ?? throw new NotFoundException("CollegeGrade", id);

        if (grade.Status != GradeStatus.Submitted)
            throw new ValidationException(new List<string>
            {
                "Only submitted grades can be rejected."
            });

        grade.Status = GradeStatus.Rejected;
        grade.RejectionNote = request.Note;

        await _gradeRepo.UpdateCollegeGradeAsync(grade);
    }
}