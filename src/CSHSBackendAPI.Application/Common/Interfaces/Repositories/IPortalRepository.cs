using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IPortalRepository
{
    Task<Student?> GetStudentByIdAsync(long studentId);
    Task<IEnumerable<BasicEdGrade>> GetPostedGradesAsync(long studentId);
    Task<IEnumerable<CollegeGrade>> GetPostedCollegeGradesAsync(long studentId);
    Task<IEnumerable<SubjectLoad>> GetScheduleAsync(string gradeLevel, string? section, string schoolYear);
}