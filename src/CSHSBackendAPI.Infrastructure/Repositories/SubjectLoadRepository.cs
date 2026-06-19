using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class SubjectLoadRepository : ISubjectLoadRepository
{
    private readonly AppDbContext _context;

    public SubjectLoadRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<SubjectLoad>> GetAllAsync(
        string? schoolYear, long? campusId, long? teacherId)
    {
        var query = _context.SubjectLoads
            .Include(s => s.Campus)
            .Where(s => s.IsActive)
            .AsQueryable();

        if (!string.IsNullOrEmpty(schoolYear))
            query = query.Where(s => s.SchoolYear == schoolYear);

        if (campusId.HasValue)
            query = query.Where(s => s.CampusId == campusId);

        if (teacherId.HasValue)
            query = query.Where(s => s.TeacherId == teacherId);

        return await query.OrderBy(s => s.Subject).ToListAsync();
    }

    public async Task<SubjectLoad?> GetByIdAsync(long id) =>
        await _context.SubjectLoads
            .Include(s => s.Campus)
            .FirstOrDefaultAsync(s => s.Id == id);

    public async Task<SubjectLoad> CreateAsync(SubjectLoad load)
    {
        _context.SubjectLoads.Add(load);
        await _context.SaveChangesAsync();
        return load;
    }

    public async Task UpdateAsync(SubjectLoad load)
    {
        _context.SubjectLoads.Update(load);
        await _context.SaveChangesAsync();
    }
}