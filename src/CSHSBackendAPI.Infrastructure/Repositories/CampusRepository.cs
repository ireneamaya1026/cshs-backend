using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class CampusRepository : ICampusRepository
{
    private readonly AppDbContext _context;

    public CampusRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Campus>> GetAllAsync() =>
        await _context.Campuses
            .Where(c => c.IsActive)
            .OrderBy(c => c.SortOrder)
            .ThenBy(c => c.Name)
            .ToListAsync();

    public async Task<Campus?> GetByIdAsync(long id) =>
        await _context.Campuses
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<bool> CampusKeyExistsAsync(string campusKey) =>
        await _context.Campuses
            .AnyAsync(c => c.CampusKey == campusKey);

    public async Task<Campus> CreateAsync(Campus campus)
    {
        _context.Campuses.Add(campus);
        await _context.SaveChangesAsync();
        return campus;
    }

    public async Task UpdateAsync(Campus campus)
    {
        _context.Campuses.Update(campus);
        await _context.SaveChangesAsync();
    }
}