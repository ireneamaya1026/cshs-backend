using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IAttendanceRepository
{
    Task<IEnumerable<AttendanceRecord>> GetAsync(long? subjectLoadId, DateOnly? date, string? section);
    Task<IEnumerable<AttendanceRecord>> GetByStudentAsync(long studentId, string schoolYear);
    Task<AttendanceRecord?> GetExistingAsync(long studentId, DateOnly date, long? subjectLoadId);
    Task<AttendanceRecord> CreateAsync(AttendanceRecord record);
    Task UpdateAsync(AttendanceRecord record);
}