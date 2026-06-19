using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Documents.DTOs;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Documents.Commands.AdvanceDocument;

public class AdvanceDocumentCommandHandler
{
    private readonly IDocumentRepository _repo;

    public AdvanceDocumentCommandHandler(IDocumentRepository repo) =>
        _repo = repo;

    public async Task<DocumentRequestDto> Handle(
        long id, AdvanceDocumentRequest request, string byName, string byRole)
    {
        var doc = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException("DocumentRequest", id);

        // Map action to next status
        var nextStatus = request.ActionId switch
        {
            "process" => DocumentRequestStatus.Processing,
            "for_payment" => DocumentRequestStatus.ForPayment,
            "ready" => DocumentRequestStatus.Ready,
            "cancel" => DocumentRequestStatus.Cancelled,
            _ => throw new ValidationException(new List<string>
            {
                $"Invalid action '{request.ActionId}'."
            })
        };

        doc.Status = nextStatus;
        await _repo.UpdateAsync(doc);

        await _repo.AddStatusHistoryAsync(new DocumentStatusHistory
        {
            RequestId = id,
            Status = nextStatus.ToString(),
            ByName = byName,
            Note = request.Note,
            RecordedAt = DateTime.UtcNow
        });

        return new DocumentRequestDto
        {
            Id = doc.Id,
            Status = doc.Status.ToString()
        };
    }
}