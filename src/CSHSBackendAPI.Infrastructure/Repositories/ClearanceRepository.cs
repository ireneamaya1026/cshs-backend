
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class ClearanceRepository : IClearanceRepository
{
    private readonly AppDbContext _context;

    public ClearanceRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Clearance>> GetAllAsync(long? studentId, string? schoolYear)
    {
        var query = _context.Clearances
            .Include(c => c.DeptSignoffs)
            .AsQueryable();

        if (studentId.HasValue)
            query = query.Where(c => c.StudentId == studentId);

        if (!string.IsNullOrEmpty(schoolYear))
            query = query.Where(c => c.SchoolYear == schoolYear);

        return await query.OrderByDescending(c => c.RequestedAt).ToListAsync();
    }

    public async Task<Clearance?> GetByIdAsync(long id) =>
        await _context.Clearances
            .Include(c => c.DeptSignoffs)
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Clearance> CreateAsync(Clearance clearance)
    {
        _context.Clearances.Add(clearance);
        await _context.SaveChangesAsync();
        return clearance;
    }

    public async Task UpdateAsync(Clearance clearance)
    {
        _context.Clearances.Update(clearance);
        await _context.SaveChangesAsync();
    }

    public async Task<ClearanceDeptSignoff?> GetSignoffAsync(long clearanceId, string deptId) =>
        await _context.ClearanceDeptSignoffs
            .FirstOrDefaultAsync(s => s.ClearanceId == clearanceId && s.DeptId == deptId);

    public async Task UpdateSignoffAsync(ClearanceDeptSignoff signoff)
    {
        _context.ClearanceDeptSignoffs.Update(signoff);
        await _context.SaveChangesAsync();
    }
}