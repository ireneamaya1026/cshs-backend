using CSHSBackendAPI.Application.Announcements.DTOs;
using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Announcements.Commands.UpdateAnnouncement;

public class UpdateAnnouncementCommandHandler
{
    private readonly IAnnouncementRepository _repo;

    public UpdateAnnouncementCommandHandler(IAnnouncementRepository repo) =>
        _repo = repo;

    public async Task<AnnouncementDto> Handle(long id, UpdateAnnouncementRequest request)
    {
        var announcement = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException("Announcement", id);

        if (request.Title != null) announcement.Title = request.Title;
        if (request.Body != null) announcement.Body = request.Body;
        if (request.Tag != null) announcement.Tag = request.Tag;
        if (request.IsPinned.HasValue) announcement.IsPinned = request.IsPinned.Value;
        if (request.PublishedAt.HasValue) announcement.PublishedAt = request.PublishedAt;
        if (request.ExpiresAt.HasValue) announcement.ExpiresAt = request.ExpiresAt;

        if (request.TargetAudience != null &&
            Enum.TryParse<AnnouncementAudience>(request.TargetAudience, true, out var audience))
            announcement.TargetAudience = audience;

        await _repo.UpdateAsync(announcement);

        return new AnnouncementDto
        {
            Id = announcement.Id,
            CampusId = announcement.CampusId,
            Title = announcement.Title,
            Body = announcement.Body,
            Tag = announcement.Tag,
            IsPinned = announcement.IsPinned,
            TargetAudience = announcement.TargetAudience.ToString(),
            PublishedAt = announcement.PublishedAt,
            ExpiresAt = announcement.ExpiresAt,
            CreatedAt = announcement.CreatedAt
        };
    }
}