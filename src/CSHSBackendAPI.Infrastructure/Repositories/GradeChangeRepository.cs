using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class GradeChangeRepository : IGradeChangeRepository
{
    private readonly AppDbContext _context;

    public GradeChangeRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<GradeChangeRequest>> GetAllAsync(
        string? status, long? campusId, string? schoolYear)
    {
        var query = _context.GradeChangeRequests.AsQueryable();

        if (!string.IsNullOrEmpty(status) &&
            Enum.TryParse<GradeChangeStatus>(status, true, out var gradeChangeStatus))
            query = query.Where(g => g.Status == gradeChangeStatus);

        if (campusId.HasValue)
            query = query.Where(g => g.CampusId == campusId);

        if (!string.IsNullOrEmpty(schoolYear))
            query = query.Where(g => g.SchoolYear == schoolYear);

        return await query
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync();
    }

    public async Task<GradeChangeRequest?> GetByIdAsync(long id) =>
        await _context.GradeChangeRequests
            .Include(g => g.Audits)
            .FirstOrDefaultAsync(g => g.Id == id);

    public async Task<GradeChangeRequest> CreateAsync(GradeChangeRequest request)
    {
        _context.GradeChangeRequests.Add(request);
        await _context.SaveChangesAsync();
        return request;
    }

    public async Task UpdateAsync(GradeChangeRequest request)
    {
        _context.GradeChangeRequests.Update(request);
        await _context.SaveChangesAsync();
    }

    public async Task AddAuditAsync(GradeChangeAudit audit)
    {
        // Append only — immutable
        _context.GradeChangeAudits.Add(audit);
        await _context.SaveChangesAsync();
    }
}