using CSHSBackendAPI.Application.Attendance.DTOs;
using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Attendance.Commands.SaveBatchAttendance;

public class SaveBatchAttendanceCommandHandler
{
    private readonly IAttendanceRepository _repo;

    public SaveBatchAttendanceCommandHandler(IAttendanceRepository repo) =>
        _repo = repo;

    public async Task Handle(BatchAttendanceRequest request)
    {
        foreach (var item in request.Records)
        {
            if (!Enum.TryParse<AttendanceStatus>(item.Status, true, out var status))
                throw new ValidationException(new List<string>
                {
                    $"Invalid attendance status '{item.Status}'."
                });

            var existing = await _repo.GetExistingAsync(
                item.StudentId, item.Date, item.SubjectLoadId);

            var absenceEquivalent = status == AttendanceStatus.Late ? 0.5m :
                                    status == AttendanceStatus.Absent ? 1m : 0m;

            if (existing != null)
            {
                existing.Status = status;
                existing.Remarks = item.Remarks;
                existing.AbsenceEquivalent = absenceEquivalent;
                await _repo.UpdateAsync(existing);
            }
            else
            {
                await _repo.CreateAsync(new AttendanceRecord
                {
                    CampusId = item.CampusId,
                    SchoolYear = item.SchoolYear,
                    StudentId = item.StudentId,
                    SubjectLoadId = item.SubjectLoadId,
                    TeacherId = item.TeacherId,
                    Section = item.Section,
                    Date = item.Date,
                    DayOfWeek = item.Date.DayOfWeek.ToString(),
                    Status = status,
                    Remarks = item.Remarks,
                    AbsenceEquivalent = absenceEquivalent
                });
            }
        }
    }
}