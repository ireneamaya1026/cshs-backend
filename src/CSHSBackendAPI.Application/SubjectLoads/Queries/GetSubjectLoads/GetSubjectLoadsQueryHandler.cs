using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.SubjectLoads.DTOs;

namespace CSHSBackendAPI.Application.SubjectLoads.Queries.GetSubjectLoads;

public class GetSubjectLoadsQueryHandler
{
    private readonly ISubjectLoadRepository _repo;

    public GetSubjectLoadsQueryHandler(ISubjectLoadRepository repo) =>
        _repo = repo;

    public async Task<IEnumerable<SubjectLoadDto>> Handle(
        string? schoolYear, long? campusId, long? teacherId)
    {
        var loads = await _repo.GetAllAsync(schoolYear, campusId, teacherId);

        return loads.Select(s => new SubjectLoadDto
        {
            Id = s.Id,
            CampusId = s.CampusId,
            CampusName = s.Campus?.Name ?? string.Empty,
            SchoolYear = s.SchoolYear,
            Department = s.Department.ToString(),
            TeacherId = s.TeacherId,
            TeacherName = s.TeacherName,
            GradeLevel = s.GradeLevel,
            Section = s.Section,
            SubjectArea = s.SubjectArea,
            Subject = s.Subject,
            Program = s.Program,
            YearLevel = s.YearLevel,
            Semester = s.Semester,
            Units = s.Units,
            ScheduleJson = s.ScheduleJson,
            IsActive = s.IsActive
        });
    }
}