using CSHSBackendAPI.Application.Common.Models;
using CSHSBackendAPI.Application.FormTemplates.Commands.UpsertFormTemplate;
using CSHSBackendAPI.Application.FormTemplates.DTOs;
using CSHSBackendAPI.Application.FormTemplates.Queries.GetFormTemplates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/form-templates")]
[Authorize]
public class FormTemplatesController : ControllerBase
{
    private readonly GetFormTemplatesQueryHandler _getHandler;
    private readonly UpsertFormTemplateCommandHandler _upsertHandler;

    public FormTemplatesController(
        GetFormTemplatesQueryHandler getHandler,
        UpsertFormTemplateCommandHandler upsertHandler)
    {
        _getHandler = getHandler;
        _upsertHandler = upsertHandler;
    }

    [HttpGet]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,registrar_college")]
    public async Task<IActionResult> GetAll([FromQuery] string? form_type)
    {
        var result = await _getHandler.Handle(form_type);
        return Ok(ApiResponse<IEnumerable<FormTemplateDto>>.Ok(result));
    }

    [HttpPost]
    [Authorize(Roles = "technical_admin")]
    public async Task<IActionResult> Upsert([FromBody] UpsertFormTemplateRequest request)
    {
        var userId = long.Parse(User.FindFirst("sub")!.Value);
        var result = await _upsertHandler.Handle(request, userId);
        return Ok(ApiResponse<FormTemplateDto>.Ok(result, "Form template saved successfully."));
    }
}