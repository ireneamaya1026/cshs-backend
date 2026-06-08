using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Students.DTOs;

namespace CSHSBackendAPI.Application.Students.Queries.GetStudentGrades;

public class GetStudentGradesQueryHandler
{
    private readonly IStudentRepository _studentRepo;

    public GetStudentGradesQueryHandler(IStudentRepository studentRepo) =>
        _studentRepo = studentRepo;

    public async Task<IEnumerable<StudentGradeDto>> Handle(long studentId)
    {
        var grades = await _studentRepo.GetGradesAsync(studentId);

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