using CSHSBackendAPI.Application.Attendance.Commands.SaveBatchAttendance;
using CSHSBackendAPI.Application.Attendance.DTOs;
using CSHSBackendAPI.Application.Attendance.Queries.GetAttendance;
using CSHSBackendAPI.Application.Attendance.Queries.GetAttendanceSummary;
using CSHSBackendAPI.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/attendance")]
[Authorize]
public class AttendanceController : ControllerBase
{
    private readonly GetAttendanceQueryHandler _getHandler;
    private readonly GetAttendanceSummaryQueryHandler _summaryHandler;
    private readonly SaveBatchAttendanceCommandHandler _batchHandler;

    public AttendanceController(
        GetAttendanceQueryHandler getHandler,
        GetAttendanceSummaryQueryHandler summaryHandler,
        SaveBatchAttendanceCommandHandler batchHandler)
    {
        _getHandler = getHandler;
        _summaryHandler = summaryHandler;
        _batchHandler = batchHandler;
    }

    [HttpGet]
    [Authorize(Roles = "technical_admin,admin,teacher,principal_basic")]
    public async Task<IActionResult> Get(
        [FromQuery] long? subject_load_id,
        [FromQuery] DateOnly? date,
        [FromQuery] string? section)
    {
        var result = await _getHandler.Handle(subject_load_id, date, section);
        return Ok(ApiResponse<IEnumerable<AttendanceDto>>.Ok(result));
    }

    [HttpPost("batch")]
    [Authorize(Roles = "technical_admin,teacher")]
    public async Task<IActionResult> SaveBatch([FromBody] BatchAttendanceRequest request)
    {
        await _batchHandler.Handle(request);
        return Ok(ApiResponse<object>.Ok("Attendance saved successfully."));
    }

    [HttpGet("summary")]
    [Authorize(Roles = "technical_admin,admin,teacher,principal_basic,registrar_basic")]
    public async Task<IActionResult> GetSummary(
        [FromQuery] long student_id,
        [FromQuery] string school_year)
    {
        var result = await _summaryHandler.Handle(student_id, school_year);
        return Ok(ApiResponse<AttendanceSummaryDto>.Ok(result));
    }
}