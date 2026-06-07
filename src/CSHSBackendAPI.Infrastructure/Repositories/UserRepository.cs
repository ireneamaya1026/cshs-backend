using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<SystemUser>> GetAllAsync(string? role, long? campusId)
    {
        var query = _context.SystemUsers
            .Include(u => u.Campus)
            .AsQueryable();

        if (!string.IsNullOrEmpty(role))
            query = query.Where(u => u.Role.ToString() == role);

        if (campusId.HasValue)
            query = query.Where(u => u.CampusId == campusId);

        return await query
            .OrderBy(u => u.Name)
            .ToListAsync();
    }

    public async Task<SystemUser?> GetByIdAsync(long id) =>
        await _context.SystemUsers
            .Include(u => u.Campus)
            .FirstOrDefaultAsync(u => u.Id == id);

    public async Task<bool> EmailExistsAsync(string email) =>
        await _context.SystemUsers
            .AnyAsync(u => u.Email == email);

    public async Task<SystemUser> CreateAsync(SystemUser user)
    {
        _context.SystemUsers.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(SystemUser user)
    {
        _context.SystemUsers.Update(user);
        await _context.SaveChangesAsync();
    }
}