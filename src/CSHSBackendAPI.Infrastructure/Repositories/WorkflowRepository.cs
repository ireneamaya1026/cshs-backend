using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class WorkflowRepository : IWorkflowRepository
{
    private readonly AppDbContext _context;

    public WorkflowRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<WorkflowDefinition>> GetAllAsync() =>
        await _context.WorkflowDefinitions.OrderBy(w => w.Label).ToListAsync();

    public async Task<WorkflowDefinition?> GetByWorkflowIdAsync(string workflowId) =>
        await _context.WorkflowDefinitions
            .FirstOrDefaultAsync(w => w.WorkflowId == workflowId);

    public async Task UpdateAsync(WorkflowDefinition workflow)
    {
        _context.WorkflowDefinitions.Update(workflow);
        await _context.SaveChangesAsync();
    }
}