using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.GradeChanges.DTOs;

namespace CSHSBackendAPI.Application.GradeChanges.Queries.GetGradeChanges;

public class GetGradeChangesQueryHandler
{
    private readonly IGradeChangeRepository _repo;

    public GetGradeChangesQueryHandler(IGradeChangeRepository repo) =>
        _repo = repo;

    public async Task<IEnumerable<GradeChangeDto>> Handle(
        string? status, long? campusId, string? schoolYear)
    {
        var items = await _repo.GetAllAsync(status, campusId, schoolYear);

        return items.Select(g => new GradeChangeDto
        {
            Id = g.Id,
            CampusId = g.CampusId,
            SchoolYear = g.SchoolYear,
            StudentId = g.StudentId,
            GradeType = g.GradeType,
            BasicGradeId = g.BasicGradeId,
            CollegeGradeId = g.CollegeGradeId,
            SubjectName = g.SubjectName,
            Period = g.Period,
            OriginalGrade = g.OriginalGrade,
            RequestedGrade = g.RequestedGrade,
            Reason = g.Reason,
            RequestedByName = g.RequestedByName,
            Status = g.Status.ToString(),
            ApprovedByName = g.ApprovedByName,
            ApprovedAt = g.ApprovedAt,
            RejectionNote = g.RejectionNote,
            PostedAt = g.PostedAt,
            CreatedAt = g.CreatedAt
        });
    }
}