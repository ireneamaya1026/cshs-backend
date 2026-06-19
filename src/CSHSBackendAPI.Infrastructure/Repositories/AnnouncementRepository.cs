using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class AnnouncementRepository : IAnnouncementRepository
{
    private readonly AppDbContext _context;

    public AnnouncementRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Announcement>> GetAllAsync(long? campusId, string? audience)
    {
        var query = _context.Announcements.AsQueryable();

        if (campusId.HasValue)
            query = query.Where(a => a.CampusId == campusId || a.CampusId == null);

        if (!string.IsNullOrEmpty(audience) &&
            Enum.TryParse<AnnouncementAudience>(audience, true, out var aud))
            query = query.Where(a => a.TargetAudience == aud || a.TargetAudience == AnnouncementAudience.All);

        return await query
            .OrderByDescending(a => a.IsPinned)
            .ThenByDescending(a => a.PublishedAt ?? a.CreatedAt)
            .ToListAsync();
    }

    public async Task<Announcement?> GetByIdAsync(long id) =>
        await _context.Announcements.FirstOrDefaultAsync(a => a.Id == id);

    public async Task<Announcement> CreateAsync(Announcement announcement)
    {
        _context.Announcements.Add(announcement);
        await _context.SaveChangesAsync();
        return announcement;
    }

    public async Task UpdateAsync(Announcement announcement)
    {
        _context.Announcements.Update(announcement);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Announcement announcement)
    {
        _context.Announcements.Remove(announcement);
        await _context.SaveChangesAsync();
    }
}