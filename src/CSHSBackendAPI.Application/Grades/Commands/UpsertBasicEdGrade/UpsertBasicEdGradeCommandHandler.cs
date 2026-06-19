using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Grades.DTOs;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Grades.Commands.UpsertBasicEdGrade;

public class UpsertBasicEdGradeCommandHandler
{
    private readonly IGradeRepository _gradeRepo;

    public UpsertBasicEdGradeCommandHandler(IGradeRepository gradeRepo) =>
        _gradeRepo = gradeRepo;

    public async Task<BasicEdGradeDto> Handle(UpsertBasicEdGradeRequest request, long teacherId, string teacherName)
    {
        if (!Enum.TryParse<GradePeriod>(request.Period, true, out var period))
            throw new ValidationException(new List<string> { $"Invalid period '{request.Period}'." });

        BasicEdGrade grade;

        // Try to find existing grade
        if (request.SubjectLoadId.HasValue)
        {
            var existing = await _gradeRepo.GetBasicEdGradeByUniqueKeyAsync(
                request.StudentId, request.SubjectLoadId.Value, request.Period);

            if (existing != null)
            {
                // Update existing draft
                if (existing.Status != GradeStatus.Draft)
                    throw new ValidationException(new List<string>
                    {
                        "Only draft grades can be updated."
                    });

                existing.WrittenWorks = request.WrittenWorks;
                existing.Performance = request.Performance;
                existing.QuarterlyExam = request.QuarterlyExam;
                existing.InitialGrade = request.InitialGrade;
                existing.Transmuted = request.Transmuted;

                await _gradeRepo.UpdateBasicEdGradeAsync(existing);
                grade = existing;
            }
            else
            {
                grade = await CreateNewBasicEdGrade(request, period, teacherId, teacherName);
            }
        }
        else
        {
            grade = await CreateNewBasicEdGrade(request, period, teacherId, teacherName);
        }

        return MapToDto(grade);
    }

    private async Task<BasicEdGrade> CreateNewBasicEdGrade(
        UpsertBasicEdGradeRequest request, GradePeriod period,
        long teacherId, string teacherName)
    {
        var grade = new BasicEdGrade
        {
            CampusId = request.CampusId,
            SchoolYear = request.SchoolYear,
            StudentId = request.StudentId,
            SubjectLoadId = request.SubjectLoadId,
            TeacherId = teacherId,
            TeacherName = teacherName,
            GradeLevel = request.GradeLevel,
            Section = request.Section,
            SubjectName = request.SubjectName,
            SubjectArea = request.SubjectArea,
            Period = period,
            WrittenWorks = request.WrittenWorks,
            Performance = request.Performance,
            QuarterlyExam = request.QuarterlyExam,
            InitialGrade = request.InitialGrade,
            Transmuted = request.Transmuted,
            Status = GradeStatus.Draft
        };

        return await _gradeRepo.CreateBasicEdGradeAsync(grade);
    }

    private BasicEdGradeDto MapToDto(BasicEdGrade g) => new()
    {
        Id = g.Id,
        StudentId = g.StudentId,
        SchoolYear = g.SchoolYear,
        GradeLevel = g.GradeLevel,
        Section = g.Section,
        SubjectName = g.SubjectName,
        SubjectArea = g.SubjectArea,
        Period = g.Period.ToString(),
        WrittenWorks = g.WrittenWorks,
        Performance = g.Performance,
        QuarterlyExam = g.QuarterlyExam,
        InitialGrade = g.InitialGrade,
        Transmuted = g.Transmuted,
        Status = g.Status.ToString(),
        TeacherName = g.TeacherName,
        TeacherId = g.TeacherId,
        SubjectLoadId = g.SubjectLoadId
    };
}