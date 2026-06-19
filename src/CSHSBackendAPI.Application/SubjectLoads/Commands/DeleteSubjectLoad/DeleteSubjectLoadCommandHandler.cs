using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;

namespace CSHSBackendAPI.Application.SubjectLoads.Commands.DeleteSubjectLoad;

public class DeleteSubjectLoadCommandHandler
{
    private readonly ISubjectLoadRepository _repo;

    public DeleteSubjectLoadCommandHandler(ISubjectLoadRepository repo) =>
        _repo = repo;

    public async Task Handle(long id)
    {
        var load = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException("SubjectLoad", id);

        load.IsActive = false;
        await _repo.UpdateAsync(load);
    }
}