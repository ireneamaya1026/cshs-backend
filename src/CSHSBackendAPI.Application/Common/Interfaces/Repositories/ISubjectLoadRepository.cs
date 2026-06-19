using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface ISubjectLoadRepository
{
    Task<IEnumerable<SubjectLoad>> GetAllAsync(
        string? schoolYear, long? campusId, long? teacherId);
    Task<SubjectLoad?> GetByIdAsync(long id);
    Task<SubjectLoad> CreateAsync(SubjectLoad load);
    Task UpdateAsync(SubjectLoad load);
}