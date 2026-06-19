using CSHSBackendAPI.Application.Attendance.DTOs;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;

namespace CSHSBackendAPI.Application.Attendance.Queries.GetAttendance;

public class GetAttendanceQueryHandler
{
    private readonly IAttendanceRepository _repo;

    public GetAttendanceQueryHandler(IAttendanceRepository repo) =>
        _repo = repo;

    public async Task<IEnumerable<AttendanceDto>> Handle(
        long? subjectLoadId, DateOnly? date, string? section)
    {
        var records = await _repo.GetAsync(subjectLoadId, date, section);

        return records.Select(a => new AttendanceDto
        {
            Id = a.Id,
            StudentId = a.StudentId,
            SchoolYear = a.SchoolYear,
            Date = a.Date,
            DayOfWeek = a.DayOfWeek,
            Status = a.Status.ToString(),
            Remarks = a.Remarks,
            AbsenceEquivalent = a.AbsenceEquivalent,
            SubjectLoadId = a.SubjectLoadId,
            Section = a.Section
        });
    }
}