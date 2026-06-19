using CSHSBackendAPI.Application.Clearances.DTOs;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Clearances.Commands.CreateClearance;

public class CreateClearanceCommandHandler
{
    private readonly IClearanceRepository _repo;

    public CreateClearanceCommandHandler(IClearanceRepository repo) =>
        _repo = repo;

    public async Task<ClearanceDto> Handle(CreateClearanceRequest request)
    {
        var clearance = new Clearance
        {
            StudentId = request.StudentId,
            CampusId = request.CampusId,
            SchoolYear = request.SchoolYear,
            Reason = request.Reason,
            RequestedAt = DateTime.UtcNow
        };

        var created = await _repo.CreateAsync(clearance);

        // Auto-create dept signoffs
        var depts = new[]
        {
            ("library", "Library"),
            ("accounting", "Accounting"),
            ("registrar", "Registrar"),
            ("guidance", "Guidance"),
            ("admin", "Admin Office")
        };

        foreach (var (deptId, deptLabel) in depts)
        {
            _repo.GetType(); // just to use repo
            created.DeptSignoffs.Add(new ClearanceDeptSignoff
            {
                ClearanceId = created.Id,
                DeptId = deptId,
                DeptLabel = deptLabel,
                IsCleared = false,
                IsAutoCleared = false
            });
        }

        await _repo.UpdateAsync(created);

        return new ClearanceDto
        {
            Id = created.Id,
            StudentId = created.StudentId,
            SchoolYear = created.SchoolYear,
            Reason = created.Reason,
            IsFullyCleared = false,
            RequestedAt = created.RequestedAt
        };
    }
}