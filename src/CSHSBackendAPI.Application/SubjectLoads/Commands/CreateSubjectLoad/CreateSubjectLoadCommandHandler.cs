using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.SubjectLoads.DTOs;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.SubjectLoads.Commands.CreateSubjectLoad;

public class CreateSubjectLoadCommandHandler
{
    private readonly ISubjectLoadRepository _repo;

    public CreateSubjectLoadCommandHandler(ISubjectLoadRepository repo) =>
        _repo = repo;

    public async Task<SubjectLoadDto> Handle(CreateSubjectLoadRequest request)
    {
        if (!Enum.TryParse<Department>(request.Department, true, out var dept))
            throw new ValidationException(new List<string>
            {
                $"Invalid department '{request.Department}'."
            });

        var load = new SubjectLoad
        {
            CampusId = request.CampusId,
            SchoolYear = request.SchoolYear,
            Department = dept,
            TeacherId = request.TeacherId,
            TeacherName = request.TeacherName,
            GradeLevel = request.GradeLevel,
            Section = request.Section,
            SubjectArea = request.SubjectArea,
            Subject = request.Subject,
            Program = request.Program,
            YearLevel = request.YearLevel,
            Semester = request.Semester,
            Units = request.Units,
            ScheduleJson = request.ScheduleJson,
            IsActive = true
        };

        var created = await _repo.CreateAsync(load);

        return new SubjectLoadDto
        {
            Id = created.Id,
            CampusId = created.CampusId,
            SchoolYear = created.SchoolYear,
            Department = created.Department.ToString(),
            TeacherId = created.TeacherId,
            TeacherName = created.TeacherName,
            GradeLevel = created.GradeLevel,
            Section = created.Section,
            Subject = created.Subject,
            IsActive = created.IsActive
        };
    }
}