using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IClearanceRepository
{
    Task<IEnumerable<Clearance>> GetAllAsync(long? studentId, string? schoolYear);
    Task<Clearance?> GetByIdAsync(long id);
    Task<Clearance> CreateAsync(Clearance clearance);
    Task UpdateAsync(Clearance clearance);
    Task<ClearanceDeptSignoff?> GetSignoffAsync(long clearanceId, string deptId);
    Task UpdateSignoffAsync(ClearanceDeptSignoff signoff);
}