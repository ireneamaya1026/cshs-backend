using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class SchoolYearRepository : ISchoolYearRepository
{
    private readonly AppDbContext _context;

    public SchoolYearRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<SchoolYear>> GetAllAsync() =>
        await _context.SchoolYears
            .OrderByDescending(s => s.YearLabel)
            .ToListAsync();

    public async Task<SchoolYear?> GetByIdAsync(long id) =>
        await _context.SchoolYears
            .FirstOrDefaultAsync(s => s.Id == id);

    public async Task<SchoolYear?> GetCurrentAsync() =>
        await _context.SchoolYears
            .FirstOrDefaultAsync(s => s.IsCurrent);

    public async Task<bool> YearLabelExistsAsync(string yearLabel) =>
        await _context.SchoolYears
            .AnyAsync(s => s.YearLabel == yearLabel);

    public async Task<SchoolYear> CreateAsync(SchoolYear schoolYear)
    {
        _context.SchoolYears.Add(schoolYear);
        await _context.SaveChangesAsync();
        return schoolYear;
    }

    public async Task UpdateAsync(SchoolYear schoolYear)
    {
        _context.SchoolYears.Update(schoolYear);
        await _context.SaveChangesAsync();
    }

    // Unset all current school years before setting a new one
    public async Task UnsetCurrentAsync()
    {
        var current = await _context.SchoolYears
            .Where(s => s.IsCurrent)
            .ToListAsync();

        foreach (var sy in current)
            sy.IsCurrent = false;

        await _context.SaveChangesAsync();
    }
}