using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.GradeChanges.DTOs;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.GradeChanges.Commands.ApproveGradeChange;

public class ApproveGradeChangeCommandHandler
{
    private readonly IGradeChangeRepository _repo;

    public ApproveGradeChangeCommandHandler(IGradeChangeRepository repo) =>
        _repo = repo;

    public async Task Handle(long id, GradeChangeActionRequest request,
        long approvedById, string approvedByName, string approvedByRole)
    {
        var gradeChange = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException("GradeChangeRequest", id);

        if (gradeChange.Status != GradeChangeStatus.Requested)
            throw new ValidationException(new List<string>
            {
                "Only requested grade changes can be approved."
            });

        gradeChange.Status = GradeChangeStatus.Approved;
        gradeChange.ApprovedById = approvedById;
        gradeChange.ApprovedByName = approvedByName;
        gradeChange.ApprovedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(gradeChange);

        await _repo.AddAuditAsync(new GradeChangeAudit
        {
            RequestId = id,
            Action = "approved",
            ByUserId = approvedById,
            ByName = approvedByName,
            ByRole = approvedByRole,
            Note = request.Note,
            RecordedAt = DateTime.UtcNow
        });
    }
}