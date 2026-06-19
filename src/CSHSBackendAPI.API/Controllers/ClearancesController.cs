using CSHSBackendAPI.Application.Clearances.Commands.CreateClearance;
using CSHSBackendAPI.Application.Clearances.Commands.SignClearance;
using CSHSBackendAPI.Application.Clearances.DTOs;
using CSHSBackendAPI.Application.Clearances.Queries.GetClearances;
using CSHSBackendAPI.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/clearances")]
[Authorize]
public class ClearancesController : ControllerBase
{
    private readonly GetClearancesQueryHandler _getHandler;
    private readonly CreateClearanceCommandHandler _createHandler;
    private readonly SignClearanceCommandHandler _signHandler;

    public ClearancesController(
        GetClearancesQueryHandler getHandler,
        CreateClearanceCommandHandler createHandler,
        SignClearanceCommandHandler signHandler)
    {
        _getHandler = getHandler;
        _createHandler = createHandler;
        _signHandler = signHandler;
    }

    [HttpGet]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,registrar_college,accounting")]
    public async Task<IActionResult> GetAll(
        [FromQuery] long? student_id,
        [FromQuery] string? school_year)
    {
        var result = await _getHandler.Handle(student_id, school_year);
        return Ok(ApiResponse<IEnumerable<ClearanceDto>>.Ok(result));
    }

    [HttpPost]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,registrar_college")]
    public async Task<IActionResult> Create([FromBody] CreateClearanceRequest request)
    {
        var result = await _createHandler.Handle(request);
        return Ok(ApiResponse<ClearanceDto>.Ok(result, "Clearance created successfully."));
    }

    [HttpPatch("{id}/sign/{dept}")]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,registrar_college,accounting")]
    public async Task<IActionResult> Sign(long id, string dept, [FromBody] SignClearanceRequest request)
    {
        var userId = long.Parse(User.FindFirst("sub")!.Value);
        var name = User.FindFirst("name")?.Value ?? string.Empty;
        await _signHandler.Handle(id, dept, request, userId, name);
        return Ok(ApiResponse<object>.Ok($"Department '{dept}' signed off successfully."));
    }
}