using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IEnrollmentRepository
{
    Task<(IEnumerable<Enrollment> Items, int Total)> GetAllAsync(
        string? schoolYear, string? currentStep,
        long? campusId, string? department,
        int page, int limit);
    Task<Enrollment?> GetByIdAsync(long id);
    Task<string> GenerateReferenceNoAsync(string schoolYear);
    Task<Enrollment> CreateAsync(Enrollment enrollment);
    Task UpdateAsync(Enrollment enrollment);
    Task AddStageHistoryAsync(EnrollmentStageHistory history);
}