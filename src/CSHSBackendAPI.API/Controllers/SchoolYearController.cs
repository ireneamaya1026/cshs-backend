using CSHSBackendAPI.Application.Common.Models;
using CSHSBackendAPI.Application.SchoolYears.Commands.CreateSchoolYear;
using CSHSBackendAPI.Application.SchoolYears.Commands.UpdateSchoolYear;
using CSHSBackendAPI.Application.SchoolYears.DTOs;
using CSHSBackendAPI.Application.SchoolYears.Queries.GetAllSchoolYears;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/school-years")]
[Authorize]
public class SchoolYearsController : ControllerBase
{
    private readonly GetAllSchoolYearsQueryHandler _getAllHandler;
    private readonly CreateSchoolYearCommandHandler _createHandler;
    private readonly UpdateSchoolYearCommandHandler _updateHandler;

    public SchoolYearsController(
        GetAllSchoolYearsQueryHandler getAllHandler,
        CreateSchoolYearCommandHandler createHandler,
        UpdateSchoolYearCommandHandler updateHandler)
    {
        _getAllHandler = getAllHandler;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
    }

    // GET /api/school-years
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAllHandler.Handle();
        return Ok(ApiResponse<IEnumerable<SchoolYearDto>>.Ok(result));
    }

    // POST /api/school-years
    [HttpPost]
    [Authorize(Roles = "technical_admin,admin")]
    public async Task<IActionResult> Create([FromBody] CreateSchoolYearRequest request)
    {
        var result = await _createHandler.Handle(request);
        return Ok(ApiResponse<SchoolYearDto>.Ok(result, "School year created successfully."));
    }

    // PATCH /api/school-years/:id
    [HttpPatch("{id}")]
    [Authorize(Roles = "technical_admin,admin")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateSchoolYearRequest request)
    {
        var result = await _updateHandler.Handle(id, request);
        return Ok(ApiResponse<SchoolYearDto>.Ok(result, "School year updated successfully."));
    }
}