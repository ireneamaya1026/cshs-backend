using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Grades.DTOs;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Grades.Commands.UpsertCollegeGrade;

public class UpsertCollegeGradeCommandHandler
{
    private readonly IGradeRepository _gradeRepo;

    public UpsertCollegeGradeCommandHandler(IGradeRepository gradeRepo) =>
        _gradeRepo = gradeRepo;

    public async Task<CollegeGradeDto> Handle(UpsertCollegeGradeRequest request, long teacherId, string teacherName)
    {
        CollegeGrade grade;

        if (request.SubjectLoadId.HasValue)
        {
            var existing = await _gradeRepo.GetCollegeGradeByUniqueKeyAsync(
                request.StudentId, request.SubjectLoadId.Value, request.Semester);

            if (existing != null)
            {
                if (existing.Status != GradeStatus.Draft)
                    throw new ValidationException(new List<string>
                    {
                        "Only draft grades can be updated."
                    });

                existing.Prelim = request.Prelim;
                existing.Midterm = request.Midterm;
                existing.Finals = request.Finals;
                existing.SemesterGrade = request.SemesterGrade;
                existing.PointGrade = request.PointGrade;
                existing.Descriptor = request.Descriptor;
                existing.SpecialGrade = request.SpecialGrade;

                await _gradeRepo.UpdateCollegeGradeAsync(existing);
                grade = existing;
            }
            else
            {
                grade = await CreateNewCollegeGrade(request, teacherId, teacherName);
            }
        }
        else
        {
            grade = await CreateNewCollegeGrade(request, teacherId, teacherName);
        }

        return MapToDto(grade);
    }

    private async Task<CollegeGrade> CreateNewCollegeGrade(
        UpsertCollegeGradeRequest request, long teacherId, string teacherName)
    {
        var grade = new CollegeGrade
        {
            CampusId = request.CampusId,
            SchoolYear = request.SchoolYear,
            Semester = request.Semester,
            StudentId = request.StudentId,
            SubjectLoadId = request.SubjectLoadId,
            TeacherId = teacherId,
            TeacherName = teacherName,
            Program = request.Program,
            YearLevel = request.YearLevel,
            SubjectName = request.SubjectName,
            Prelim = request.Prelim,
            Midterm = request.Midterm,
            Finals = request.Finals,
            SemesterGrade = request.SemesterGrade,
            PointGrade = request.PointGrade,
            Descriptor = request.Descriptor,
            SpecialGrade = request.SpecialGrade,
            Status = GradeStatus.Draft
        };

        return await _gradeRepo.CreateCollegeGradeAsync(grade);
    }

    private CollegeGradeDto MapToDto(CollegeGrade g) => new()
    {
        Id = g.Id,
        StudentId = g.StudentId,
        SchoolYear = g.SchoolYear,
        Semester = g.Semester,
        SubjectName = g.SubjectName,
        Program = g.Program,
        YearLevel = g.YearLevel,
        Prelim = g.Prelim,
        Midterm = g.Midterm,
        Finals = g.Finals,
        SemesterGrade = g.SemesterGrade,
        PointGrade = g.PointGrade,
        Descriptor = g.Descriptor,
        SpecialGrade = g.SpecialGrade,
        Status = g.Status.ToString(),
        TeacherName = g.TeacherName,
        SubjectLoadId = g.SubjectLoadId
    };
}