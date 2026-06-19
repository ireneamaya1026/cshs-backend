using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.FormTemplates.DTOs;

namespace CSHSBackendAPI.Application.FormTemplates.Queries.GetFormTemplates;

public class GetFormTemplatesQueryHandler
{
    private readonly IFormTemplateRepository _repo;

    public GetFormTemplatesQueryHandler(IFormTemplateRepository repo) =>
        _repo = repo;

    public async Task<IEnumerable<FormTemplateDto>> Handle(string? formType)
    {
        var items = await _repo.GetAllAsync(formType);

        return items.Select(f => new FormTemplateDto
        {
            Id = f.Id,
            FormType = f.FormType,
            Label = f.Label,
            FieldsJson = f.FieldsJson,
            IsActive = f.IsActive
        });
    }
}