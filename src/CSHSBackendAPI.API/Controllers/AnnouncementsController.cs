using CSHSBackendAPI.Application.Announcements.Commands.CreateAnnouncement;
using CSHSBackendAPI.Application.Announcements.Commands.DeleteAnnouncement;
using CSHSBackendAPI.Application.Announcements.Commands.UpdateAnnouncement;
using CSHSBackendAPI.Application.Announcements.DTOs;
using CSHSBackendAPI.Application.Announcements.Queries.GetAnnouncements;
using CSHSBackendAPI.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/announcements")]
[Authorize]
public class AnnouncementsController : ControllerBase
{
    private readonly GetAnnouncementsQueryHandler _getHandler;
    private readonly CreateAnnouncementCommandHandler _createHandler;
    private readonly UpdateAnnouncementCommandHandler _updateHandler;
    private readonly DeleteAnnouncementCommandHandler _deleteHandler;

    public AnnouncementsController(
        GetAnnouncementsQueryHandler getHandler,
        CreateAnnouncementCommandHandler createHandler,
        UpdateAnnouncementCommandHandler updateHandler,
        DeleteAnnouncementCommandHandler deleteHandler)
    {
        _getHandler = getHandler;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] long? campus_id,
        [FromQuery] string? audience)
    {
        var result = await _getHandler.Handle(campus_id, audience);
        return Ok(ApiResponse<IEnumerable<AnnouncementDto>>.Ok(result));
    }

    [HttpPost]
    [Authorize(Roles = "technical_admin,admin")]
    public async Task<IActionResult> Create([FromBody] CreateAnnouncementRequest request)
    {
        var userId = long.Parse(User.FindFirst("sub")!.Value);
        var result = await _createHandler.Handle(request, userId);
        return Ok(ApiResponse<AnnouncementDto>.Ok(result, "Announcement created successfully."));
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "technical_admin,admin")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateAnnouncementRequest request)
    {
        var result = await _updateHandler.Handle(id, request);
        return Ok(ApiResponse<AnnouncementDto>.Ok(result, "Announcement updated successfully."));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "technical_admin,admin")]
    public async Task<IActionResult> Delete(long id)
    {
        await _deleteHandler.Handle(id);
        return Ok(ApiResponse<object>.Ok("Announcement deleted successfully."));
    }
}