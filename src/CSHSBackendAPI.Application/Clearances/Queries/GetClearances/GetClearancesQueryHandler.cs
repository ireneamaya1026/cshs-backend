using CSHSBackendAPI.Application.Clearances.DTOs;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;

namespace CSHSBackendAPI.Application.Clearances.Queries.GetClearances;

public class GetClearancesQueryHandler
{
    private readonly IClearanceRepository _repo;

    public GetClearancesQueryHandler(IClearanceRepository repo) =>
        _repo = repo;

    public async Task<IEnumerable<ClearanceDto>> Handle(long? studentId, string? schoolYear)
    {
        var items = await _repo.GetAllAsync(studentId, schoolYear);

        return items.Select(c => new ClearanceDto
        {
            Id = c.Id,
            CampusId = c.CampusId,
            StudentId = c.StudentId,
            SchoolYear = c.SchoolYear,
            Reason = c.Reason,
            HasUnpaidBalance = c.HasUnpaidBalance,
            IsFullyCleared = c.IsFullyCleared,
            RequestedAt = c.RequestedAt,
            CompletedAt = c.CompletedAt,
            DeptSignoffs = c.DeptSignoffs?.Select(s => new DeptSignoffDto
            {
                Id = s.Id,
                DeptId = s.DeptId,
                DeptLabel = s.DeptLabel,
                IsCleared = s.IsCleared,
                ClearedByName = s.ClearedByName,
                ClearedAt = s.ClearedAt,
                IsAutoCleared = s.IsAutoCleared
            }).ToList() ?? new()
        });
    }
}