using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly AppDbContext _context;

    public DocumentRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<DocumentRequest>> GetAllAsync(
        string? status, long? campusId, string? dept)
    {
        var query = _context.DocumentRequests.AsQueryable();

        if (!string.IsNullOrEmpty(status) &&
            Enum.TryParse<DocumentRequestStatus>(status, true, out var docStatus))
            query = query.Where(d => d.Status == docStatus);

        if (campusId.HasValue)
            query = query.Where(d => d.CampusId == campusId);

        if (!string.IsNullOrEmpty(dept) &&
            Enum.TryParse<Department>(dept, true, out var department))
            query = query.Where(d => d.Department == department);

        return await query
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync();
    }

    public async Task<DocumentRequest?> GetByIdAsync(long id) =>
        await _context.DocumentRequests
            .Include(d => d.StatusHistories)
            .FirstOrDefaultAsync(d => d.Id == id);

    public async Task<DocumentRequest> CreateAsync(DocumentRequest request)
    {
        _context.DocumentRequests.Add(request);
        await _context.SaveChangesAsync();
        return request;
    }

    public async Task UpdateAsync(DocumentRequest request)
    {
        _context.DocumentRequests.Update(request);
        await _context.SaveChangesAsync();
    }

    public async Task AddStatusHistoryAsync(DocumentStatusHistory history)
    {
        // Append only — immutable
        _context.DocumentStatusHistories.Add(history);
        await _context.SaveChangesAsync();
    }
}