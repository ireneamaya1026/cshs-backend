using CSHSBackendAPI.Application.Common.Models;
using CSHSBackendAPI.Application.Documents.Commands.AdvanceDocument;
using CSHSBackendAPI.Application.Documents.Commands.CreateDocument;
using CSHSBackendAPI.Application.Documents.Commands.ReleaseDocument;
using CSHSBackendAPI.Application.Documents.DTOs;
using CSHSBackendAPI.Application.Documents.Queries.GetDocuments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/documents")]
[Authorize]
public class DocumentsController : ControllerBase
{
    private readonly GetDocumentsQueryHandler _getHandler;
    private readonly CreateDocumentCommandHandler _createHandler;
    private readonly AdvanceDocumentCommandHandler _advanceHandler;
    private readonly ReleaseDocumentCommandHandler _releaseHandler;

    public DocumentsController(
        GetDocumentsQueryHandler getHandler,
        CreateDocumentCommandHandler createHandler,
        AdvanceDocumentCommandHandler advanceHandler,
        ReleaseDocumentCommandHandler releaseHandler)
    {
        _getHandler = getHandler;
        _createHandler = createHandler;
        _advanceHandler = advanceHandler;
        _releaseHandler = releaseHandler;
    }

    [HttpGet]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,registrar_college")]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? status,
        [FromQuery] long? campus_id,
        [FromQuery] string? dept)
    {
        var result = await _getHandler.Handle(status, campus_id, dept);
        return Ok(ApiResponse<IEnumerable<DocumentRequestDto>>.Ok(result));
    }

    [HttpPost]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,registrar_college")]
    public async Task<IActionResult> Create([FromBody] CreateDocumentRequest request)
    {
        var name = User.FindFirst("name")?.Value ?? string.Empty;
        var role = User.FindFirst("role")?.Value ?? string.Empty;
        var result = await _createHandler.Handle(request, name, role);
        return Ok(ApiResponse<DocumentRequestDto>.Ok(result, "Document request created."));
    }

    [HttpPatch("{id}/advance")]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,registrar_college")]
    public async Task<IActionResult> Advance(long id, [FromBody] AdvanceDocumentRequest request)
    {
        var name = User.FindFirst("name")?.Value ?? string.Empty;
        var role = User.FindFirst("role")?.Value ?? string.Empty;
        var result = await _advanceHandler.Handle(id, request, name, role);
        return Ok(ApiResponse<DocumentRequestDto>.Ok(result, "Document status updated."));
    }

    [HttpPatch("{id}/release")]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,registrar_college")]
    public async Task<IActionResult> Release(long id, [FromBody] ReleaseDocumentRequest request)
    {
        var userId = long.Parse(User.FindFirst("sub")!.Value);
        var name = User.FindFirst("name")?.Value ?? string.Empty;
        var role = User.FindFirst("role")?.Value ?? string.Empty;
        await _releaseHandler.Handle(id, request, userId, name, role);
        return Ok(ApiResponse<object>.Ok("Document released successfully."));
    }
}