using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IWorkflowRepository
{
    Task<IEnumerable<WorkflowDefinition>> GetAllAsync();
    Task<WorkflowDefinition?> GetByWorkflowIdAsync(string workflowId);
    Task UpdateAsync(WorkflowDefinition workflow);
}