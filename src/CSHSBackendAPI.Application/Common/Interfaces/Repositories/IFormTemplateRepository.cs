using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IFormTemplateRepository
{
    Task<IEnumerable<FormTemplate>> GetAllAsync(string? formType);
    Task<FormTemplate?> GetByFormTypeAsync(string formType);
    Task<FormTemplate> CreateAsync(FormTemplate template);
    Task UpdateAsync(FormTemplate template);
}