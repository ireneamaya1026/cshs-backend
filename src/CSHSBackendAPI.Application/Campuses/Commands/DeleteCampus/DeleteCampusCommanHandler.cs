using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;

namespace CSHSBackendAPI.Application.Campuses.Commands.DeleteCampus;

public class DeleteCampusCommandHandler
{
    private readonly ICampusRepository _campusRepo;

    public DeleteCampusCommandHandler(ICampusRepository campusRepo) =>
        _campusRepo = campusRepo;

    public async Task Handle(long id)
    {
        var campus = await _campusRepo.GetByIdAsync(id)
            ?? throw new NotFoundException("Campus", id);

        // Soft delete
        campus.IsActive = false;
        await _campusRepo.UpdateAsync(campus);
    }
}