using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface ISchoolRepository
{
    Task<School?> GetByIdAsync(int id);
    Task<School?> GetBySlugAsync(string slug);
    Task<IEnumerable<School>> GetAllAsync();
    Task<School> CreateAsync(School school);
    Task UpdateAsync(School school);
    Task DeleteAsync(int id);
    Task<bool> SlugExistsAsync(string slug);
}