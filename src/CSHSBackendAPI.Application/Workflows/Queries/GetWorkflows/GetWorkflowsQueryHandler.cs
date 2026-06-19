using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Workflows.DTOs;

namespace CSHSBackendAPI.Application.Workflows.Queries.GetWorkflows;

public class GetWorkflowsQueryHandler
{
    private readonly IWorkflowRepository _repo;

    public GetWorkflowsQueryHandler(IWorkflowRepository repo) =>
        _repo = repo;

    public async Task<IEnumerable<WorkflowDto>> Handle()
    {
        var workflows = await _repo.GetAllAsync();

        return workflows.Select(w => new WorkflowDto
        {
            Id = w.Id,
            WorkflowId = w.WorkflowId,
            Label = w.Label,
            Department = w.Department.ToString(),
            Version = w.Version,
            StepsJson = w.StepsJson,
            PermissionsJson = w.PermissionsJson,
            IsLocked = w.IsLocked
        });
    }
}