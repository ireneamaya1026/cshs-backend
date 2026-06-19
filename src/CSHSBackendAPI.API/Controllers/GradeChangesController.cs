using CSHSBackendAPI.Application.Common.Models;
using CSHSBackendAPI.Application.GradeChanges.Commands.ApproveGradeChange;
using CSHSBackendAPI.Application.GradeChanges.Commands.PostGradeChange;
using CSHSBackendAPI.Application.GradeChanges.Commands.RejectGradeChange;
using CSHSBackendAPI.Application.GradeChanges.Commands.SubmitGradeChange;
using CSHSBackendAPI.Application.GradeChanges.DTOs;
using CSHSBackendAPI.Application.GradeChanges.Queries.GetGradeChanges;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/grade-changes")]
[Authorize]
public class GradeChangesController : ControllerBase
{
    private readonly GetGradeChangesQueryHandler _getHandler;
    private readonly SubmitGradeChangeCommandHandler _submitHandler;
    private readonly ApproveGradeChangeCommandHandler _approveHandler;
    private readonly PostGradeChangeCommandHandler _postHandler;
    private readonly RejectGradeChangeCommandHandler _rejectHandler;

    public GradeChangesController(
        GetGradeChangesQueryHandler getHandler,
        SubmitGradeChangeCommandHandler submitHandler,
        ApproveGradeChangeCommandHandler approveHandler,
        PostGradeChangeCommandHandler postHandler,
        RejectGradeChangeCommandHandler rejectHandler)
    {
        _getHandler = getHandler;
        _submitHandler = submitHandler;
        _approveHandler = approveHandler;
        _postHandler = postHandler;
        _rejectHandler = rejectHandler;
    }

    [HttpGet]
    [Authorize(Roles = "technical_admin,admin,principal_basic,program_head,registrar_basic,registrar_college")]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? status,
        [FromQuery] long? campus_id,
        [FromQuery] string? school_year)
    {
        var result = await _getHandler.Handle(status, campus_id, school_year);
        return Ok(ApiResponse<IEnumerable<GradeChangeDto>>.Ok(result));
    }

    [HttpPost]
    [Authorize(Roles = "technical_admin,teacher")]
    public async Task<IActionResult> Submit([FromBody] SubmitGradeChangeRequest request)
    {
        var userId = long.Parse(User.FindFirst("sub")!.Value);
        var name = User.FindFirst("name")?.Value ?? string.Empty;
        var role = User.FindFirst("role")?.Value ?? string.Empty;
        var result = await _submitHandler.Handle(request, userId, name, role);
        return Ok(ApiResponse<GradeChangeDto>.Ok(result, "Grade change request submitted."));
    }

    [HttpPatch("{id}/approve")]
    [Authorize(Roles = "technical_admin,principal_basic,program_head")]
    public async Task<IActionResult> Approve(long id, [FromBody] GradeChangeActionRequest request)
    {
        var userId = long.Parse(User.FindFirst("sub")!.Value);
        var name = User.FindFirst("name")?.Value ?? string.Empty;
        var role = User.FindFirst("role")?.Value ?? string.Empty;
        await _approveHandler.Handle(id, request, userId, name, role);
        return Ok(ApiResponse<object>.Ok("Grade change approved."));
    }

    [HttpPatch("{id}/post")]
    [Authorize(Roles = "technical_admin,registrar_basic,registrar_college")]
    public async Task<IActionResult> Post(long id)
    {
        var userId = long.Parse(User.FindFirst("sub")!.Value);
        var name = User.FindFirst("name")?.Value ?? string.Empty;
        var role = User.FindFirst("role")?.Value ?? string.Empty;
        await _postHandler.Handle(id, userId, name, role);
        return Ok(ApiResponse<object>.Ok("Grade change posted."));
    }

    [HttpPatch("{id}/reject")]
    [Authorize(Roles = "technical_admin,principal_basic,program_head")]
    public async Task<IActionResult> Reject(long id, [FromBody] GradeChangeActionRequest request)
    {
        var userId = long.Parse(User.FindFirst("sub")!.Value);
        var name = User.FindFirst("name")?.Value ?? string.Empty;
        var role = User.FindFirst("role")?.Value ?? string.Empty;
        await _rejectHandler.Handle(id, request, userId, name, role);
        return Ok(ApiResponse<object>.Ok("Grade change rejected."));
    }
}