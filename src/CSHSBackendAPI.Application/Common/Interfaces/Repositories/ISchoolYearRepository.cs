using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface ISchoolYearRepository
{
    Task<IEnumerable<SchoolYear>> GetAllAsync();
    Task<SchoolYear?> GetByIdAsync(long id);
    Task<SchoolYear?> GetCurrentAsync();
    Task<bool> YearLabelExistsAsync(string yearLabel);
    Task<SchoolYear> CreateAsync(SchoolYear schoolYear);
    Task UpdateAsync(SchoolYear schoolYear);
    Task UnsetCurrentAsync();
}