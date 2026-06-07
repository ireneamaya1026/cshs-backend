using CSHSBackendAPI.Application.Campuses.Commands.CreateCampus;
using CSHSBackendAPI.Application.Campuses.Commands.DeleteCampus;
using CSHSBackendAPI.Application.Campuses.Commands.UpdateCampus;
using CSHSBackendAPI.Application.Campuses.DTOs;
using CSHSBackendAPI.Application.Campuses.Queries.GetAllCampuses;
using CSHSBackendAPI.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/campuses")]
[Authorize]
public class CampusesController : ControllerBase
{
    private readonly GetAllCampusesQueryHandler _getAllHandler;
    private readonly CreateCampusCommandHandler _createHandler;
    private readonly UpdateCampusCommandHandler _updateHandler;
    private readonly DeleteCampusCommandHandler _deleteHandler;

    public CampusesController(
        GetAllCampusesQueryHandler getAllHandler,
        CreateCampusCommandHandler createHandler,
        UpdateCampusCommandHandler updateHandler,
        DeleteCampusCommandHandler deleteHandler)
    {
        _getAllHandler = getAllHandler;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    // GET /api/campuses
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAllHandler.Handle();
        return Ok(ApiResponse<IEnumerable<CampusDto>>.Ok(result));
    }

    // POST /api/campuses
    [HttpPost]
    [Authorize(Roles = "technical_admin")]
    public async Task<IActionResult> Create([FromBody] CreateCampusRequest request)
    {
        var result = await _createHandler.Handle(request);
        return Ok(ApiResponse<CampusDto>.Ok(result, "Campus created successfully."));
    }

    // PATCH /api/campuses/:id
    [HttpPatch("{id}")]
    [Authorize(Roles = "technical_admin")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateCampusRequest request)
    {
        var result = await _updateHandler.Handle(id, request);
        return Ok(ApiResponse<CampusDto>.Ok(result, "Campus updated successfully."));
    }

    // DELETE /api/campuses/:id
    [HttpDelete("{id}")]
    [Authorize(Roles = "technical_admin")]
    public async Task<IActionResult> Delete(long id)
    {
        await _deleteHandler.Handle(id);
        return Ok(ApiResponse<object>.Ok("Campus deactivated successfully."));
    }
}