using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IAuthRepository
{
    Task<SystemUser?> GetByEmailAsync(string email, string schoolSlug);
    Task<SystemUser?> GetByIdAsync(int id);
    Task<SystemUser> CreateAsync(SystemUser user);
    Task UpdateAsync(SystemUser user);
}