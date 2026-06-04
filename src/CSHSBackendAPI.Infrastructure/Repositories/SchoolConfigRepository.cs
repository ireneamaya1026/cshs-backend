using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class SchoolConfigRepository : ISchoolConfigRepository
{
    private readonly AppDbContext _context;

    public SchoolConfigRepository(AppDbContext context) => _context = context;

    public async Task<SchoolConfig?> GetAsync() =>
        await _context.SchoolConfigs.FirstOrDefaultAsync();

    public async Task UpdateAsync(SchoolConfig config)
    {
        _context.SchoolConfigs.Update(config);
        await _context.SaveChangesAsync();
    }
}