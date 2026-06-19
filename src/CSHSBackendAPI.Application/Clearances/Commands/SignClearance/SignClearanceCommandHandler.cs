using CSHSBackendAPI.Application.Clearances.DTOs;
using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;

namespace CSHSBackendAPI.Application.Clearances.Commands.SignClearance;

public class SignClearanceCommandHandler
{
    private readonly IClearanceRepository _repo;

    public SignClearanceCommandHandler(IClearanceRepository repo) =>
        _repo = repo;

    public async Task Handle(long clearanceId, string deptId,
        SignClearanceRequest request, long clearedById, string clearedByName)
    {
        var signoff = await _repo.GetSignoffAsync(clearanceId, deptId)
            ?? throw new NotFoundException("ClearanceDeptSignoff", $"{clearanceId}/{deptId}");

        // Once cleared, no updates allowed
        if (signoff.IsCleared)
            throw new ValidationException(new List<string>
            {
                "This department has already signed off. Contact Super Admin to override."
            });

        signoff.IsCleared = true;
        signoff.ClearedById = clearedById;
        signoff.ClearedByName = clearedByName;
        signoff.ClearedAt = DateTime.UtcNow;

        await _repo.UpdateSignoffAsync(signoff);

        // Check if all depts cleared
        var clearance = await _repo.GetByIdAsync(clearanceId);
        if (clearance != null && clearance.DeptSignoffs.All(s => s.IsCleared))
        {
            clearance.IsFullyCleared = true;
            clearance.CompletedAt = DateTime.UtcNow;
            await _repo.UpdateAsync(clearance);
        }
    }
}