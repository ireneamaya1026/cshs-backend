using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface ICampusRepository
{
    Task<IEnumerable<Campus>> GetAllAsync();
    Task<Campus?> GetByIdAsync(long id);
    Task<bool> CampusKeyExistsAsync(string campusKey);
    Task<Campus> CreateAsync(Campus campus);
    Task UpdateAsync(Campus campus);
}