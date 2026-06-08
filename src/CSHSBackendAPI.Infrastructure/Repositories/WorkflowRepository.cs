using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class WorkflowRepository : IWorkflowRepository
{
    private readonly AppDbContext _context;

    public WorkflowRepository(AppDbContext context) => _context = context;

    public async Task<WorkflowDefinition?> GetByWorkflowIdAsync(string workflowId) =>
        await _context.WorkflowDefinitions
            .FirstOrDefaultAsync(w => w.WorkflowId == workflowId && !w.IsLocked);
}