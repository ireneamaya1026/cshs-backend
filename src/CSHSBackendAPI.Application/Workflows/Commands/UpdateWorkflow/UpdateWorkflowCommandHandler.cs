using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Workflows.DTOs;

namespace CSHSBackendAPI.Application.Workflows.Commands.UpdateWorkflow;

public class UpdateWorkflowCommandHandler
{
    private readonly IWorkflowRepository _repo;

    public UpdateWorkflowCommandHandler(IWorkflowRepository repo) =>
        _repo = repo;

    public async Task<WorkflowDto> Handle(string workflowId, UpdateWorkflowRequest request)
    {
        var workflow = await _repo.GetByWorkflowIdAsync(workflowId)
            ?? throw new NotFoundException("Workflow", workflowId);

        if (workflow.IsLocked)
            throw new ValidationException(new List<string>
            {
                "This workflow is locked and cannot be modified."
            });

        if (request.StepsJson != null) workflow.StepsJson = request.StepsJson;
        if (request.PermissionsJson != null) workflow.PermissionsJson = request.PermissionsJson;
        workflow.Version++;

        await _repo.UpdateAsync(workflow);

        return new WorkflowDto
        {
            Id = workflow.Id,
            WorkflowId = workflow.WorkflowId,
            Label = workflow.Label,
            Department = workflow.Department.ToString(),
            Version = workflow.Version,
            StepsJson = workflow.StepsJson,
            PermissionsJson = workflow.PermissionsJson,
            IsLocked = workflow.IsLocked
        };
    }
}