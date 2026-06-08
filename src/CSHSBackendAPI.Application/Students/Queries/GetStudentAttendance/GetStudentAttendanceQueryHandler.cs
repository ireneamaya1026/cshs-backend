using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Students.DTOs;

namespace CSHSBackendAPI.Application.Students.Queries.GetStudentAttendance;

public class GetStudentAttendanceQueryHandler
{
    private readonly IStudentRepository _studentRepo;

    public GetStudentAttendanceQueryHandler(IStudentRepository studentRepo) =>
        _studentRepo = studentRepo;

    public async Task<IEnumerable<StudentAttendanceDto>> Handle(long studentId)
    {
        var records = await _studentRepo.GetAttendanceAsync(studentId);

        return records.Select(a => new StudentAttendanceDto
        {
            Id = a.Id,
            SchoolYear = a.SchoolYear,
            Date = a.Date,
            DayOfWeek = a.DayOfWeek,
            Status = a.Status.ToString(),
            Remarks = a.Remarks,
            AbsenceEquivalent = a.AbsenceEquivalent,
            Section = a.Section
        });
    }
}