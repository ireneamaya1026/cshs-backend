using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IDocumentRepository
{
    Task<IEnumerable<DocumentRequest>> GetAllAsync(
        string? status, long? campusId, string? dept);
    Task<DocumentRequest?> GetByIdAsync(long id);
    Task<DocumentRequest> CreateAsync(DocumentRequest request);
    Task UpdateAsync(DocumentRequest request);
    Task AddStatusHistoryAsync(DocumentStatusHistory history);
}