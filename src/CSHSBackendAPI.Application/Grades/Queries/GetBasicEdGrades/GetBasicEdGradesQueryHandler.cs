using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Grades.DTOs;

namespace CSHSBackendAPI.Application.Grades.Queries.GetBasicEdGrades;

public class GetBasicEdGradesQueryHandler
{
    private readonly IGradeRepository _gradeRepo;

    public GetBasicEdGradesQueryHandler(IGradeRepository gradeRepo) =>
        _gradeRepo = gradeRepo;

    public async Task<IEnumerable<BasicEdGradeDto>> Handle(long? subjectLoadId, string? period)
    {
        var grades = await _gradeRepo.GetBasicEdGradesAsync(subjectLoadId, period);

        return grades.Select(g => new BasicEdGradeDto
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
            SubjectLoadId = g.SubjectLoadId,
            SubmittedAt = g.SubmittedAt,
            ApprovedAt = g.ApprovedAt,
            RejectionNote = g.RejectionNote
        });
    }
}