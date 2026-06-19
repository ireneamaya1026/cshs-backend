using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Portal.DTOs;

namespace CSHSBackendAPI.Application.Portal.Queries.GetMySchedule;

public class GetMyScheduleQueryHandler
{
    private readonly IPortalRepository _repo;

    public GetMyScheduleQueryHandler(IPortalRepository repo) =>
        _repo = repo;

    public async Task<IEnumerable<MyScheduleDto>> Handle(long studentId)
    {
        var student = await _repo.GetStudentByIdAsync(studentId)
            ?? throw new NotFoundException("Student", studentId);

        var schedule = await _repo.GetScheduleAsync(
            student.GradeLevel, student.Section, student.SchoolYear);

        return schedule.Select(s => new MyScheduleDto
        {
            Subject = s.Subject,
            TeacherName = s.TeacherName,
            ScheduleJson = s.ScheduleJson
        });
    }
}