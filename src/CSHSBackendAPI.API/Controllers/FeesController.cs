using CSHSBackendAPI.Application.Common.Models;
using CSHSBackendAPI.Application.Fees.Commands.CreateFee;
using CSHSBackendAPI.Application.Fees.Commands.DeleteFee;
using CSHSBackendAPI.Application.Fees.Commands.UpdateFee;
using CSHSBackendAPI.Application.Fees.DTOs;
using CSHSBackendAPI.Application.Fees.Queries.GetFees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/fees")]
[Authorize]
public class FeesController : ControllerBase
{
    private readonly GetFeesQueryHandler _getHandler;
    private readonly CreateFeeCommandHandler _createHandler;
    private readonly UpdateFeeCommandHandler _updateHandler;
    private readonly DeleteFeeCommandHandler _deleteHandler;

    public FeesController(
        GetFeesQueryHandler getHandler,
        CreateFeeCommandHandler createHandler,
        UpdateFeeCommandHandler updateHandler,
        DeleteFeeCommandHandler deleteHandler)
    {
        _getHandler = getHandler;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    [HttpGet]
    [Authorize(Roles = "technical_admin,admin,accounting,registrar_basic,registrar_college")]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? school_year,
        [FromQuery] string? grade_level)
    {
        var result = await _getHandler.Handle(school_year, grade_level);
        return Ok(ApiResponse<IEnumerable<FeeStructureDto>>.Ok(result));
    }

    [HttpPost]
    [Authorize(Roles = "technical_admin,admin,accounting")]
    public async Task<IActionResult> Create([FromBody] CreateFeeRequest request)
    {
        var result = await _createHandler.Handle(request);
        return Ok(ApiResponse<FeeStructureDto>.Ok(result, "Fee structure created successfully."));
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "technical_admin,admin,accounting")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateFeeRequest request)
    {
        var result = await _updateHandler.Handle(id, request);
        return Ok(ApiResponse<FeeStructureDto>.Ok(result, "Fee structure updated successfully."));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "technical_admin,admin,accounting")]
    public async Task<IActionResult> Delete(long id)
    {
        await _deleteHandler.Handle(id);
        return Ok(ApiResponse<object>.Ok("Fee structure deactivated."));
    }
}