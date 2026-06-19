using CSHSBackendAPI.Application.Attendance.DTOs;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Attendance.Queries.GetAttendanceSummary;

public class GetAttendanceSummaryQueryHandler
{
    private readonly IAttendanceRepository _repo;

    public GetAttendanceSummaryQueryHandler(IAttendanceRepository repo) =>
        _repo = repo;

    public async Task<AttendanceSummaryDto> Handle(long studentId, string schoolYear)
    {
        var records = await _repo.GetByStudentAsync(studentId, schoolYear);
        var list = records.ToList();

        return new AttendanceSummaryDto
        {
            StudentId = studentId,
            SchoolYear = schoolYear,
            TotalDays = list.Count,
            Present = list.Count(a => a.Status == AttendanceStatus.Present),
            Absent = list.Count(a => a.Status == AttendanceStatus.Absent),
            Late = list.Count(a => a.Status == AttendanceStatus.Late),
            Excused = list.Count(a => a.Status == AttendanceStatus.Excused),
            TotalAbsenceEquivalent = list.Sum(a => a.AbsenceEquivalent)
        };
    }
}