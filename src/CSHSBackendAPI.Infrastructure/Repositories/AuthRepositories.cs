using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context) => _context = context;

    public async Task<SystemUser?> GetByEmailAsync(string email, string schoolSlug) =>
        await _context.SystemUsers
            .Include(u => u.Campus)
            .FirstOrDefaultAsync(u =>
                u.Email == email &&
                u.IsActive);

    public async Task<SystemUser?> GetByIdAsync(int id) =>
        await _context.SystemUsers
            .Include(u => u.Campus)
            .FirstOrDefaultAsync(u => u.Id == id && u.IsActive);

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