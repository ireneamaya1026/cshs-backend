using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IWorkflowAuditRepository
{
    Task<WorkflowAudit> CreateAsync(WorkflowAudit audit);
}