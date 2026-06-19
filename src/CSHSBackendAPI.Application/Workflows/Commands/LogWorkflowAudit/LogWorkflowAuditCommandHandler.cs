using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Workflows.DTOs;
using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Workflows.Commands.LogWorkflowAudit;

public class LogWorkflowAuditCommandHandler
{
    private readonly IWorkflowAuditRepository _repo;

    public LogWorkflowAuditCommandHandler(IWorkflowAuditRepository repo) =>
        _repo = repo;

    public async Task Handle(LogWorkflowAuditRequest request, string byName, string byRole)
    {
        await _repo.CreateAsync(new WorkflowAudit
        {
            WorkflowId = request.WorkflowId,
            EntityType = request.EntityType,
            EntityId = request.EntityId,
            FromStep = request.FromStep,
            ToStep = request.ToStep,
            ActionId = request.ActionId,
            ByName = byName,
            ByRole = byRole,
            Note = request.Note,
            RecordedAt = DateTime.UtcNow
        });
    }
}