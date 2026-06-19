using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IFeeRepository
{
    Task<IEnumerable<FeeStructure>> GetAllAsync(string? schoolYear, string? gradeLevel);
    Task<FeeStructure?> GetByIdAsync(long id);
    Task<FeeStructure> CreateAsync(FeeStructure fee);
    Task UpdateAsync(FeeStructure fee);
}