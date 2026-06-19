using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class WorkflowAuditRepository : IWorkflowAuditRepository
{
    private readonly AppDbContext _context;

    public WorkflowAuditRepository(AppDbContext context) => _context = context;

    // Append only — never update or delete
    public async Task<WorkflowAudit> CreateAsync(WorkflowAudit audit)
    {
        _context.WorkflowAudits.Add(audit);
        await _context.SaveChangesAsync();
        return audit;
    }
}