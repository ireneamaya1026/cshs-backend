using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.SubjectLoads.DTOs;

namespace CSHSBackendAPI.Application.SubjectLoads.Commands.UpdateSubjectLoad;

public class UpdateSubjectLoadCommandHandler
{
    private readonly ISubjectLoadRepository _repo;

    public UpdateSubjectLoadCommandHandler(ISubjectLoadRepository repo) =>
        _repo = repo;

    public async Task<SubjectLoadDto> Handle(long id, UpdateSubjectLoadRequest request)
    {
        var load = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException("SubjectLoad", id);

        if (request.GradeLevel != null) load.GradeLevel = request.GradeLevel;
        if (request.Section != null) load.Section = request.Section;
        if (request.SubjectArea != null) load.SubjectArea = request.SubjectArea;
        if (request.Subject != null) load.Subject = request.Subject;
        if (request.Program != null) load.Program = request.Program;
        if (request.YearLevel != null) load.YearLevel = request.YearLevel;
        if (request.Semester != null) load.Semester = request.Semester;
        if (request.Units.HasValue) load.Units = request.Units;
        if (request.ScheduleJson != null) load.ScheduleJson = request.ScheduleJson;
        if (request.TeacherName != null) load.TeacherName = request.TeacherName;

        await _repo.UpdateAsync(load);

        return new SubjectLoadDto
        {
            Id = load.Id,
            CampusId = load.CampusId,
            CampusName = load.Campus?.Name ?? string.Empty,
            SchoolYear = load.SchoolYear,
            Department = load.Department.ToString(),
            TeacherId = load.TeacherId,
            TeacherName = load.TeacherName,
            GradeLevel = load.GradeLevel,
            Section = load.Section,
            SubjectArea = load.SubjectArea,
            Subject = load.Subject,
            IsActive = load.IsActive
        };
    }
}