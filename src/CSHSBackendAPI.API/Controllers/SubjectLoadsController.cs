using CSHSBackendAPI.Application.Common.Models;
using CSHSBackendAPI.Application.SubjectLoads.Commands.CreateSubjectLoad;
using CSHSBackendAPI.Application.SubjectLoads.Commands.DeleteSubjectLoad;
using CSHSBackendAPI.Application.SubjectLoads.Commands.UpdateSubjectLoad;
using CSHSBackendAPI.Application.SubjectLoads.DTOs;
using CSHSBackendAPI.Application.SubjectLoads.Queries.GetSubjectLoads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/subject-loads")]
[Authorize]
public class SubjectLoadsController : ControllerBase
{
    private readonly GetSubjectLoadsQueryHandler _getHandler;
    private readonly CreateSubjectLoadCommandHandler _createHandler;
    private readonly UpdateSubjectLoadCommandHandler _updateHandler;
    private readonly DeleteSubjectLoadCommandHandler _deleteHandler;

    public SubjectLoadsController(
        GetSubjectLoadsQueryHandler getHandler,
        CreateSubjectLoadCommandHandler createHandler,
        UpdateSubjectLoadCommandHandler updateHandler,
        DeleteSubjectLoadCommandHandler deleteHandler)
    {
        _getHandler = getHandler;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    [HttpGet]
    [Authorize(Roles = "technical_admin,admin,teacher,principal_basic,program_head,registrar_basic,registrar_college")]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? school_year,
        [FromQuery] long? campus_id,
        [FromQuery] long? teacher_id)
    {
        var result = await _getHandler.Handle(school_year, campus_id, teacher_id);
        return Ok(ApiResponse<IEnumerable<SubjectLoadDto>>.Ok(result));
    }

    [HttpPost]
    [Authorize(Roles = "technical_admin,admin,principal_basic,program_head")]
    public async Task<IActionResult> Create([FromBody] CreateSubjectLoadRequest request)
    {
        var result = await _createHandler.Handle(request);
        return Ok(ApiResponse<SubjectLoadDto>.Ok(result, "Subject load created successfully."));
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "technical_admin,admin,principal_basic,program_head")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateSubjectLoadRequest request)
    {
        var result = await _updateHandler.Handle(id, request);
        return Ok(ApiResponse<SubjectLoadDto>.Ok(result, "Subject load updated successfully."));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "technical_admin,admin")]
    public async Task<IActionResult> Delete(long id)
    {
        await _deleteHandler.Handle(id);
        return Ok(ApiResponse<object>.Ok("Subject load removed successfully."));
    }
}