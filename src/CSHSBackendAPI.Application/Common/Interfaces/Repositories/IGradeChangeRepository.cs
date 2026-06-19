using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IGradeChangeRepository
{
    Task<IEnumerable<GradeChangeRequest>> GetAllAsync(
        string? status, long? campusId, string? schoolYear);
    Task<GradeChangeRequest?> GetByIdAsync(long id);
    Task<GradeChangeRequest> CreateAsync(GradeChangeRequest request);
    Task UpdateAsync(GradeChangeRequest request);
    Task AddAuditAsync(GradeChangeAudit audit);
}