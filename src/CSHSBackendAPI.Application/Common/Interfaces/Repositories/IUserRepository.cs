using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<SystemUser>> GetAllAsync(string? role, long? campusId);
    Task<SystemUser?> GetByIdAsync(long id);
    Task<bool> EmailExistsAsync(string email);
    Task<SystemUser> CreateAsync(SystemUser user);
    Task UpdateAsync(SystemUser user);
}