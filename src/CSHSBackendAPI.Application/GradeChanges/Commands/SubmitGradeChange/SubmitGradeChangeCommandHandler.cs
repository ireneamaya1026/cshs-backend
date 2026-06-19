using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.GradeChanges.DTOs;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.GradeChanges.Commands.SubmitGradeChange;

public class SubmitGradeChangeCommandHandler
{
    private readonly IGradeChangeRepository _repo;

    public SubmitGradeChangeCommandHandler(IGradeChangeRepository repo) =>
        _repo = repo;

    public async Task<GradeChangeDto> Handle(
        SubmitGradeChangeRequest request, long requestedById,
        string requestedByName, string requestedByRole)
    {
        var gradeChange = new GradeChangeRequest
        {
            CampusId = request.CampusId,
            SchoolYear = request.SchoolYear,
            StudentId = request.StudentId,
            GradeType = request.GradeType,
            BasicGradeId = request.BasicGradeId,
            CollegeGradeId = request.CollegeGradeId,
            SubjectName = request.SubjectName,
            Period = request.Period,
            OriginalGrade = request.OriginalGrade,
            RequestedGrade = request.RequestedGrade,
            Reason = request.Reason,
            RequestedById = requestedById,
            RequestedByName = requestedByName,
            Status = GradeChangeStatus.Requested
        };

        var created = await _repo.CreateAsync(gradeChange);

        // Append audit
        await _repo.AddAuditAsync(new GradeChangeAudit
        {
            RequestId = created.Id,
            Action = "submitted",
            ByUserId = requestedById,
            ByName = requestedByName,
            ByRole = requestedByRole,
            RecordedAt = DateTime.UtcNow
        });

        return new GradeChangeDto
        {
            Id = created.Id,
            Status = created.Status.ToString(),
            RequestedByName = created.RequestedByName,
            SubjectName = created.SubjectName,
            RequestedGrade = created.RequestedGrade,
            CreatedAt = created.CreatedAt
        };
    }
}