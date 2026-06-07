using CSHSBackendAPI.Application.Config.Commands.UpdateConfig;
using CSHSBackendAPI.Application.Config.DTOs;
using CSHSBackendAPI.Application.Config.Queries.GetConfig;
using CSHSBackendAPI.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/config")]
[Authorize]
public class ConfigController : ControllerBase
{
    private readonly GetConfigQueryHandler _getHandler;
    private readonly UpdateConfigCommandHandler _updateHandler;

    public ConfigController(
        GetConfigQueryHandler getHandler,
        UpdateConfigCommandHandler updateHandler)
    {
        _getHandler = getHandler;
        _updateHandler = updateHandler;
    }
    // GET /api/config
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _getHandler.Handle();
        return Ok(ApiResponse<SchoolConfigDto>.Ok(result));
    }

    // PATCH /api/config
    [HttpPatch]
    [Authorize(Roles = "technical_admin")]
    public async Task<IActionResult> Update([FromBody] UpdateConfigRequest request)
    {
        var result = await _updateHandler.Handle(request);
        return Ok(ApiResponse<SchoolConfigDto>.Ok(result, "School config updated successfully."));
    }
}