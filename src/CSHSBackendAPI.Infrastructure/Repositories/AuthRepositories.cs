using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context) => _context = context;

    public async Task<User?> GetByEmailAsync(string email, string schoolSlug) =>
        await _context.Users
            .Include(u => u.Campus)
            .FirstOrDefaultAsync(u =>
                u.Email == email &&
                u.SchoolSlug == schoolSlug &&
                !u.IsDeleted);

    public async Task<User?> GetByIdAsync(int id) =>
        await _context.Users
            .Include(u => u.Campus)
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}