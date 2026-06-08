using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IStudentRepository
{
    Task<(IEnumerable<Student> Items, int Total)> GetAllAsync(
        string? gradeLevel, string? section,
        long? campusId, string? schoolYear,
        string? status, int page, int limit);
    Task<Student?> GetByIdAsync(long id);
    Task<IEnumerable<BasicEdGrade>> GetGradesAsync(long studentId);
    Task<IEnumerable<AttendanceRecord>> GetAttendanceAsync(long studentId);
    Task UpdateAsync(Student student);
}