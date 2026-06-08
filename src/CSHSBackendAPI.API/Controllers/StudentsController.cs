using CSHSBackendAPI.Application.Common.Models;
using CSHSBackendAPI.Application.Students.Commands.UpdateStudent;
using CSHSBackendAPI.Application.Students.DTOs;
using CSHSBackendAPI.Application.Students.Queries.GetAllStudents;
using CSHSBackendAPI.Application.Students.Queries.GetStudentAttendance;
using CSHSBackendAPI.Application.Students.Queries.GetStudentById;
using CSHSBackendAPI.Application.Students.Queries.GetStudentGrades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/students")]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly GetAllStudentsQueryHandler _getAllHandler;
    private readonly GetStudentByIdQueryHandler _getByIdHandler;
    private readonly GetStudentGradesQueryHandler _getGradesHandler;
    private readonly GetStudentAttendanceQueryHandler _getAttendanceHandler;
    private readonly UpdateStudentCommandHandler _updateHandler;

    public StudentsController(
        GetAllStudentsQueryHandler getAllHandler,
        GetStudentByIdQueryHandler getByIdHandler,
        GetStudentGradesQueryHandler getGradesHandler,
        GetStudentAttendanceQueryHandler getAttendanceHandler,
        UpdateStudentCommandHandler updateHandler)
    {
        _getAllHandler = getAllHandler;
        _getByIdHandler = getByIdHandler;
        _getGradesHandler = getGradesHandler;
        _getAttendanceHandler = getAttendanceHandler;
        _updateHandler = updateHandler;
    }

    // GET /api/students
    [HttpGet]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,registrar_college,principal_basic,program_head")]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? grade_level,
        [FromQuery] string? section,
        [FromQuery] long? campus_id,
        [FromQuery] string? school_year,
        [FromQuery] string? status,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 20)
    {
        var result = await _getAllHandler.Handle(
            grade_level, section, campus_id, school_year, status, page, limit);
        return Ok(ApiResponse<object>.Ok(result));
    }

    // GET /api/students/:id
    [HttpGet("{id}")]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,registrar_college,principal_basic,program_head,teacher")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _getByIdHandler.Handle(id);
        return Ok(ApiResponse<StudentDto>.Ok(result));
    }

    // PATCH /api/students/:id
    [HttpPatch("{id}")]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,registrar_college")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateStudentRequest request)
    {
        var result = await _updateHandler.Handle(id, request);
        return Ok(ApiResponse<StudentDto>.Ok(result, "Student updated successfully."));
    }

    // GET /api/students/:id/grades
    [HttpGet("{id}/grades")]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,principal_basic,teacher")]
    public async Task<IActionResult> GetGrades(long id)
    {
        var result = await _getGradesHandler.Handle(id);
        return Ok(ApiResponse<IEnumerable<StudentGradeDto>>.Ok(result));
    }

    // GET /api/students/:id/attendance
    [HttpGet("{id}/attendance")]
    [Authorize(Roles = "technical_admin,admin,registrar_basic,principal_basic,teacher")]
    public async Task<IActionResult> GetAttendance(long id)
    {
        var result = await _getAttendanceHandler.Handle(id);
        return Ok(ApiResponse<IEnumerable<StudentAttendanceDto>>.Ok(result));
    }
}