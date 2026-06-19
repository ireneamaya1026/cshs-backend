using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Documents.DTOs;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Documents.Commands.ReleaseDocument;

public class ReleaseDocumentCommandHandler
{
    private readonly IDocumentRepository _repo;

    public ReleaseDocumentCommandHandler(IDocumentRepository repo) =>
        _repo = repo;

    public async Task Handle(long id, ReleaseDocumentRequest request,
        long releasedById, string byName, string byRole)
    {
        var doc = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException("DocumentRequest", id);

        if (doc.Status != DocumentRequestStatus.Ready)
            throw new ValidationException(new List<string>
            {
                "Only ready documents can be released."
            });

        doc.Status = DocumentRequestStatus.Released;
        doc.ReleasedTo = request.ReleasedTo;
        doc.ClaimSlip = request.ClaimSlip;
        doc.ReleasedAt = DateTime.UtcNow;
        doc.ReleasedById = releasedById;

        await _repo.UpdateAsync(doc);

        await _repo.AddStatusHistoryAsync(new DocumentStatusHistory
        {
            RequestId = id,
            Status = "released",
            ByName = byName,
            Note = $"Released to {request.ReleasedTo}",
            RecordedAt = DateTime.UtcNow
        });
    }
}