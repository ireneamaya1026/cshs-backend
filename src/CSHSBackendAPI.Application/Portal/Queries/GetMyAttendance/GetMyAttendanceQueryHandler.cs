using CSHSBackendAPI.Application.Attendance.DTOs;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Portal.Queries.GetMyAttendance;

public class GetMyAttendanceQueryHandler
{
    private readonly IAttendanceRepository _attendanceRepo;

    public GetMyAttendanceQueryHandler(IAttendanceRepository attendanceRepo) =>
        _attendanceRepo = attendanceRepo;

    public async Task<AttendanceSummaryDto> Handle(long studentId, string schoolYear)
    {
        var records = await _attendanceRepo.GetByStudentAsync(studentId, schoolYear);
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