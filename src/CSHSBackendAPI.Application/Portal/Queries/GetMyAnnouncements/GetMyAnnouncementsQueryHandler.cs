using CSHSBackendAPI.Application.Announcements.DTOs;
using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;

namespace CSHSBackendAPI.Application.Portal.Queries.GetMyAnnouncements;

public class GetMyAnnouncementsQueryHandler
{
    private readonly IAnnouncementRepository _announcementRepo;
    private readonly IPortalRepository _portalRepo;

    public GetMyAnnouncementsQueryHandler(
        IAnnouncementRepository announcementRepo,
        IPortalRepository portalRepo)
    {
        _announcementRepo = announcementRepo;
        _portalRepo = portalRepo;
    }

    public async Task<IEnumerable<AnnouncementDto>> Handle(long studentId)
    {
        var student = await _portalRepo.GetStudentByIdAsync(studentId)
            ?? throw new NotFoundException("Student", studentId);

        var announcements = await _announcementRepo.GetAllAsync(student.CampusId, "students");

        return announcements.Select(a => new AnnouncementDto
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