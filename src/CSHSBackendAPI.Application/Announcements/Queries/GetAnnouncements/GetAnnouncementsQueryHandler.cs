using CSHSBackendAPI.Application.Announcements.DTOs;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;

namespace CSHSBackendAPI.Application.Announcements.Queries.GetAnnouncements;

public class GetAnnouncementsQueryHandler
{
    private readonly IAnnouncementRepository _repo;

    public GetAnnouncementsQueryHandler(IAnnouncementRepository repo) =>
        _repo = repo;

    public async Task<IEnumerable<AnnouncementDto>> Handle(long? campusId, string? audience)
    {
        var items = await _repo.GetAllAsync(campusId, audience);

        return items.Select(a => new AnnouncementDto
        {
            Id = a.Id,
            CampusId = a.CampusId,
            Title = a.Title,
            Body = a.Body,
            Tag = a.Tag,
            IsPinned = a.IsPinned,
            TargetAudience = a.TargetAudience.ToString(),
            PublishedAt = a.PublishedAt,
            ExpiresAt = a.ExpiresAt,
            CreatedAt = a.CreatedAt
        });
    }
}