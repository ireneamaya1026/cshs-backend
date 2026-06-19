using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Students.DTOs;

namespace CSHSBackendAPI.Application.Portal.Queries.GetMyGrades;

public class GetMyGradesQueryHandler
{
    private readonly IPortalRepository _repo;

    public GetMyGradesQueryHandler(IPortalRepository repo) =>
        _repo = repo;

    public async Task<IEnumerable<StudentGradeDto>> Handle(long studentId)
    {
        var grades = await _repo.GetPostedGradesAsync(studentId);

        return grades.Select(g => new StudentGradeDto
        {
            Id = g.Id,
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
            TeacherName = g.TeacherName
        });
    }
}