using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Documents.DTOs;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Documents.Commands.CreateDocument;

public class CreateDocumentCommandHandler
{
    private readonly IDocumentRepository _repo;

    public CreateDocumentCommandHandler(IDocumentRepository repo) =>
        _repo = repo;

    public async Task<DocumentRequestDto> Handle(
        CreateDocumentRequest request, string byName, string byRole)
    {
        if (!Enum.TryParse<Department>(request.Department, true, out var dept))
            throw new ValidationException(new List<string>
            {
                $"Invalid department '{request.Department}'."
            });

        var doc = new DocumentRequest
        {
            StudentId = request.StudentId,
            CampusId = request.CampusId,
            SchoolYear = request.SchoolYear,
            Department = dept,
            DocumentType = request.DocumentType,
            DocumentLabel = request.DocumentLabel,
            Purpose = request.Purpose,
            RequestedBy = request.RequestedBy,
            RequestorName = request.RequestorName,
            Fee = request.Fee,
            RequiresClearance = request.RequiresClearance,
            Status = DocumentRequestStatus.Requested
        };

        var created = await _repo.CreateAsync(doc);

        // Append status history
        await _repo.AddStatusHistoryAsync(new DocumentStatusHistory
        {
            RequestId = created.Id,
            Status = "requested",
            ByName = byName,
            Note = "Document request created.",
            RecordedAt = DateTime.UtcNow
        });

        return new DocumentRequestDto
        {
            Id = created.Id,
            StudentId = created.StudentId,
            DocumentType = created.DocumentType,
            DocumentLabel = created.DocumentLabel,
            Fee = created.Fee,
            Status = created.Status.ToString(),
            CreatedAt = created.CreatedAt
        };
    }
}