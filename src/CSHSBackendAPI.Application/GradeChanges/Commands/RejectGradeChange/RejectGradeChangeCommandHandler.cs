using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.GradeChanges.DTOs;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.GradeChanges.Commands.RejectGradeChange;

public class RejectGradeChangeCommandHandler
{
    private readonly IGradeChangeRepository _repo;

    public RejectGradeChangeCommandHandler(IGradeChangeRepository repo) =>
        _repo = repo;

    public async Task Handle(long id, GradeChangeActionRequest request,
        long rejectedById, string rejectedByName, string rejectedByRole)
    {
        var gradeChange = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException("GradeChangeRequest", id);

        if (gradeChange.Status != GradeChangeStatus.Requested &&
            gradeChange.Status != GradeChangeStatus.Approved)
            throw new ValidationException(new List<string>
            {
                "This grade change cannot be rejected."
            });

        gradeChange.Status = GradeChangeStatus.Rejected;
        gradeChange.RejectionNote = request.Note;

        await _repo.UpdateAsync(gradeChange);

        await _repo.AddAuditAsync(new GradeChangeAudit
        {
            RequestId = id,
            Action = "rejected",
            ByUserId = rejectedById,
            ByName = rejectedByName,
            ByRole = rejectedByRole,
            Note = request.Note,
            RecordedAt = DateTime.UtcNow
        });
    }
}