using CSHSBackendAPI.Application.Announcements.DTOs;
using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Announcements.Commands.CreateAnnouncement;

public class CreateAnnouncementCommandHandler
{
    private readonly IAnnouncementRepository _repo;

    public CreateAnnouncementCommandHandler(IAnnouncementRepository repo) =>
        _repo = repo;

    public async Task<AnnouncementDto> Handle(CreateAnnouncementRequest request, long createdById)
    {
        if (!Enum.TryParse<AnnouncementAudience>(request.TargetAudience, true, out var audience))
            throw new ValidationException(new List<string>
            {
                $"Invalid audience '{request.TargetAudience}'."
            });

        var announcement = new Announcement
        {
            CampusId = request.CampusId,
            Title = request.Title,
            Body = request.Body,
            Tag = request.Tag,
            IsPinned = request.IsPinned,
            TargetAudience = audience,
            PublishedAt = request.PublishedAt ?? DateTime.UtcNow,
            ExpiresAt = request.ExpiresAt,
            CreatedById = createdById
        };

        var created = await _repo.CreateAsync(announcement);

        return new AnnouncementDto
        {
            Id = created.Id,
            CampusId = created.CampusId,
            Title = created.Title,
            Body = created.Body,
            Tag = created.Tag,
            IsPinned = created.IsPinned,
            TargetAudience = created.TargetAudience.ToString(),
            PublishedAt = created.PublishedAt,
            ExpiresAt = created.ExpiresAt,
            CreatedAt = created.CreatedAt
        };
    }
}