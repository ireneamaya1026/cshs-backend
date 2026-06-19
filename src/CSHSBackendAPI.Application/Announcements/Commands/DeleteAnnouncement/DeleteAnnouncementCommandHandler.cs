using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;

namespace CSHSBackendAPI.Application.Announcements.Commands.DeleteAnnouncement;

public class DeleteAnnouncementCommandHandler
{
    private readonly IAnnouncementRepository _repo;

    public DeleteAnnouncementCommandHandler(IAnnouncementRepository repo) =>
        _repo = repo;

    public async Task Handle(long id)
    {
        var announcement = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException("Announcement", id);

        await _repo.DeleteAsync(announcement);
    }
}