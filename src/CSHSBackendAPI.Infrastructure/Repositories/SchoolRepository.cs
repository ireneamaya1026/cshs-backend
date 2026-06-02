using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class SchoolRepository : ISchoolRepository
{
    private readonly AppDbContext _context;

    public SchoolRepository(AppDbContext context) => _context = context;

    public async Task<School?> GetByIdAsync(int id) =>
        await _context.Schools
            .Include(s => s.Campuses)
            .FirstOrDefaultAsync(s => s.Id == id);

    public async Task<School?> GetBySlugAsync(string slug) =>
        await _context.Schools
            .Include(s => s.Campuses)
            .FirstOrDefaultAsync(s => s.ShortName == slug);

    public async Task<IEnumerable<School>> GetAllAsync() =>
        await _context.Schools
            .Include(s => s.Campuses)
            .Where(s => s.IsActive)
            .ToListAsync();

    public async Task<School> CreateAsync(School school)
    {
        _context.Schools.Add(school);
        await _context.SaveChangesAsync();
        return school;
    }

    public async Task UpdateAsync(School school)
    {
        _context.Schools.Update(school);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var school = await GetByIdAsync(id);
        if (school is not null)
        {
            school.IsActive = false;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> SlugExistsAsync(string slug) =>
        await _context.Schools.AnyAsync(s => s.ShortName == slug);
}