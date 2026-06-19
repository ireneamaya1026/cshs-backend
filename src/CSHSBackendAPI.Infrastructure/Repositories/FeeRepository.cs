using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class FeeRepository : IFeeRepository
{
    private readonly AppDbContext _context;

    public FeeRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<FeeStructure>> GetAllAsync(string? schoolYear, string? gradeLevel)
    {
        var query = _context.FeeStructures
            .Where(f => f.IsActive)
            .AsQueryable();

        if (!string.IsNullOrEmpty(schoolYear))
            query = query.Where(f => f.SchoolYear == schoolYear);

        if (!string.IsNullOrEmpty(gradeLevel))
            query = query.Where(f => f.GradeLevel == gradeLevel);

        return await query.OrderBy(f => f.GradeLevel).ToListAsync();
    }

    public async Task<FeeStructure?> GetByIdAsync(long id) =>
        await _context.FeeStructures.FirstOrDefaultAsync(f => f.Id == id);

    public async Task<FeeStructure> CreateAsync(FeeStructure fee)
    {
        _context.FeeStructures.Add(fee);
        await _context.SaveChangesAsync();
        return fee;
    }

    public async Task UpdateAsync(FeeStructure fee)
    {
        _context.FeeStructures.Update(fee);
        await _context.SaveChangesAsync();
    }
}