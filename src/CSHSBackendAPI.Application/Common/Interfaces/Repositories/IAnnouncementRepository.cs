using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IAnnouncementRepository
{
    Task<IEnumerable<Announcement>> GetAllAsync(long? campusId, string? audience);
    Task<Announcement?> GetByIdAsync(long id);
    Task<Announcement> CreateAsync(Announcement announcement);
    Task UpdateAsync(Announcement announcement);
    Task DeleteAsync(Announcement announcement);
}