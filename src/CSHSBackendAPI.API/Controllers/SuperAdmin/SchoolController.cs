using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Common.Models;
using CSHSBackendAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers.SuperAdmin;

[ApiController]
[Route("api/superadmin/[controller]")]
[Authorize(Roles = "SuperAdmin")]
public class SchoolsController : ControllerBase
{
    private readonly ISchoolRepository _schoolRepo;

    public SchoolsController(ISchoolRepository schoolRepo) =>
        _schoolRepo = schoolRepo;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var schools = await _schoolRepo.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<School>>.Ok(schools));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var school = await _schoolRepo.GetByIdAsync(id);
        return Ok(ApiResponse<School>.Ok(school!));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] School school)
    {
        // Check slug is unique
        if (await _schoolRepo.SlugExistsAsync(school.Slug))
            return BadRequest(ApiResponse<object>.Fail($"Slug '{school.Slug}' is already taken."));

        var created = await _schoolRepo.CreateAsync(school);
        return CreatedAtAction(nameof(GetById),
            new { id = created.Id },
            ApiResponse<School>.Ok(created, "School created successfully."));
    }
}