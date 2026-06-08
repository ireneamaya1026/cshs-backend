using CSHSBackendAPI.Application.Common.Models;
using CSHSBackendAPI.Application.Enrollments.Commands.AdvanceEnrollment;
using CSHSBackendAPI.Application.Enrollments.Commands.CreateEnrollment;
using CSHSBackendAPI.Application.Enrollments.DTOs;
using CSHSBackendAPI.Application.Enrollments.Queries.GetAllEnrollments;
using CSHSBackendAPI.Application.Enrollments.Queries.GetEnrollmentById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/enrollments")]
[Authorize]
public class EnrollmentsController : ControllerBase
{
    private readonly GetAllEnrollmentsQueryHandler _getAllHandler;
    private readonly GetEnrollmentByIdQueryHandler _getByIdHandler;
    private readonly CreateEnrollmentCommandHandler _createHandler;
    private readonly AdvanceEnrollmentCommandHandler _advanceHandler;

    public EnrollmentsController(
        GetAllEnrollmentsQueryHandler getAllHandler,
        GetEnrollmentByIdQueryHandler getByIdHandler,
        CreateEnrollmentCommandHandler createHandler,
        AdvanceEnrollmentCommandHandler advanceHandler)
    {
        _getAllHandler = getAllHandler;
        _getByIdHandler = getByIdHandler;
        _createHandler = createHandler;
        _advanceHandler = advanceHandler;
    }

    // GET /api/enrollments
    [HttpGet]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,registrar_college")]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? school_year,
        [FromQuery] string? status,
        [FromQuery] long? campus_id,
        [FromQuery] string? dept,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 20)
    {
        var result = await _getAllHandler.Handle(school_year, status, campus_id, dept, page, limit);
        return Ok(ApiResponse<object>.Ok(result));
    }

    // GET /api/enrollments/:id
    [HttpGet("{id}")]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,registrar_college")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _getByIdHandler.Handle(id);
        return Ok(ApiResponse<EnrollmentDto>.Ok(result));
    }

    // POST /api/enrollments
    [HttpPost]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,registrar_college")]
    public async Task<IActionResult> Create([FromBody] CreateEnrollmentRequest request)
    {
        var byName = User.FindFirst("name")?.Value ?? "System";
        var byRole = User.FindFirst("role")?.Value ?? "admin";

        var result = await _createHandler.Handle(request, byName, byRole);
        return Ok(ApiResponse<EnrollmentDto>.Ok(result, "Enrollment created successfully."));
    }

    // PATCH /api/enrollments/:id/advance
    [HttpPatch("{id}/advance")]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,registrar_college")]
    public async Task<IActionResult> Advance(long id, [FromBody] AdvanceEnrollmentRequest request)
    {
        var byName = User.FindFirst("name")?.Value ?? "System";
        var byRole = User.FindFirst("role")?.Value ?? "admin";

        var result = await _advanceHandler.Handle(id, request, byName, byRole);
        return Ok(ApiResponse<EnrollmentDto>.Ok(result, "Enrollment advanced successfully."));
    }
}