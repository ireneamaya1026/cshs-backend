using CSHSBackendAPI.Application.Common.Models;
using CSHSBackendAPI.Application.Workflows.Commands.LogWorkflowAudit;
using CSHSBackendAPI.Application.Workflows.Commands.UpdateWorkflow;
using CSHSBackendAPI.Application.Workflows.DTOs;
using CSHSBackendAPI.Application.Workflows.Queries.GetWorkflows;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/workflows")]
[Authorize]
public class WorkflowsController : ControllerBase
{
    private readonly GetWorkflowsQueryHandler _getHandler;
    private readonly UpdateWorkflowCommandHandler _updateHandler;
    private readonly LogWorkflowAuditCommandHandler _logHandler;

    public WorkflowsController(
        GetWorkflowsQueryHandler getHandler,
        UpdateWorkflowCommandHandler updateHandler,
        LogWorkflowAuditCommandHandler logHandler)
    {
        _getHandler = getHandler;
        _updateHandler = updateHandler;
        _logHandler = logHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getHandler.Handle();
        return Ok(ApiResponse<IEnumerable<WorkflowDto>>.Ok(result));
    }

    [HttpPatch("{workflowId}")]
    [Authorize(Roles = "technical_admin")]
    public async Task<IActionResult> Update(string workflowId, [FromBody] UpdateWorkflowRequest request)
    {
        var result = await _updateHandler.Handle(workflowId, request);
        return Ok(ApiResponse<WorkflowDto>.Ok(result, "Workflow updated successfully."));
    }

    [HttpPost("audit")]
    [Authorize]
    public async Task<IActionResult> LogAudit([FromBody] LogWorkflowAuditRequest request)
    {
        var name = User.FindFirst("name")?.Value ?? string.Empty;
        var role = User.FindFirst("role")?.Value ?? string.Empty;
        await _logHandler.Handle(request, name, role);
        return Ok(ApiResponse<object>.Ok("Workflow transition logged."));
    }
}