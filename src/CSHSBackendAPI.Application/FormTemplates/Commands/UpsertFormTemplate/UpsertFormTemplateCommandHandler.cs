using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.FormTemplates.DTOs;
using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.FormTemplates.Commands.UpsertFormTemplate;

public class UpsertFormTemplateCommandHandler
{
    private readonly IFormTemplateRepository _repo;

    public UpsertFormTemplateCommandHandler(IFormTemplateRepository repo) =>
        _repo = repo;

    public async Task<FormTemplateDto> Handle(UpsertFormTemplateRequest request, long createdById)
    {
        var existing = await _repo.GetByFormTypeAsync(request.FormType);

        if (existing != null)
        {
            existing.Label = request.Label;
            existing.FieldsJson = request.FieldsJson;
            await _repo.UpdateAsync(existing);

            return new FormTemplateDto
            {
                Id = existing.Id,
                FormType = existing.FormType,
                Label = existing.Label,
                FieldsJson = existing.FieldsJson,
                IsActive = existing.IsActive
            };
        }

        var template = new FormTemplate
        {
            FormType = request.FormType,
            Label = request.Label,
            FieldsJson = request.FieldsJson,
            CreatedById = createdById,
            IsActive = true
        };

        var created = await _repo.CreateAsync(template);

        return new FormTemplateDto
        {
            Id = created.Id,
            FormType = created.FormType,
            Label = created.Label,
            FieldsJson = created.FieldsJson,
            IsActive = created.IsActive
        };
    }
}