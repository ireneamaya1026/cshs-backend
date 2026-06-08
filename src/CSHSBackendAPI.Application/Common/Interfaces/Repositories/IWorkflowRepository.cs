using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IWorkflowRepository
{
    Task<WorkflowDefinition?> GetByWorkflowIdAsync(string workflowId);
}