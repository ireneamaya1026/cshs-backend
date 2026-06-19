using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Grades.DTOs;

namespace CSHSBackendAPI.Application.Grades.Queries.GetCollegeGrades;

public class GetCollegeGradesQueryHandler
{
    private readonly IGradeRepository _gradeRepo;

    public GetCollegeGradesQueryHandler(IGradeRepository gradeRepo) =>
        _gradeRepo = gradeRepo;

    public async Task<IEnumerable<CollegeGradeDto>> Handle(long? subjectLoadId, string? semester)
    {
        var grades = await _gradeRepo.GetCollegeGradesAsync(subjectLoadId, semester);

        return grades.Select(g => new CollegeGradeDto
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
            SubjectLoadId = g.SubjectLoadId,
            SubmittedAt = g.SubmittedAt,
            ApprovedAt = g.ApprovedAt,
            RejectionNote = g.RejectionNote
        });
    }
}