using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Documents.DTOs;

namespace CSHSBackendAPI.Application.Documents.Queries.GetDocuments;

public class GetDocumentsQueryHandler
{
    private readonly IDocumentRepository _repo;

    public GetDocumentsQueryHandler(IDocumentRepository repo) =>
        _repo = repo;

    public async Task<IEnumerable<DocumentRequestDto>> Handle(
        string? status, long? campusId, string? dept)
    {
        var items = await _repo.GetAllAsync(status, campusId, dept);

        return items.Select(d => new DocumentRequestDto
        {
            Id = d.Id,
            CampusId = d.CampusId,
            SchoolYear = d.SchoolYear,
            StudentId = d.StudentId,
            Department = d.Department.ToString(),
            DocumentType = d.DocumentType,
            DocumentLabel = d.DocumentLabel,
            Purpose = d.Purpose,
            RequestedBy = d.RequestedBy,
            RequestorName = d.RequestorName,
            Fee = d.Fee,
            PaidAt = d.PaidAt,
            RequiresClearance = d.RequiresClearance,
            Status = d.Status.ToString(),
            ReleasedTo = d.ReleasedTo,
            ClaimSlip = d.ClaimSlip,
            ReleasedAt = d.ReleasedAt,
            CreatedAt = d.CreatedAt
        });
    }
}