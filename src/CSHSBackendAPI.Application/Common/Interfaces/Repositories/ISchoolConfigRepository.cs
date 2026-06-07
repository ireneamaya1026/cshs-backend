using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface ISchoolConfigRepository
{
    Task<SchoolConfig?> GetAsync();
    Task UpdateAsync(SchoolConfig config);
}