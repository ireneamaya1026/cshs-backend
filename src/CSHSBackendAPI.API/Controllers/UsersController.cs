using System.Security.Claims;
using CSHSBackendAPI.Application.Common.Models;
using CSHSBackendAPI.Application.Users.Commands.CreateUser;
using CSHSBackendAPI.Application.Users.Commands.ResetPassword;
using CSHSBackendAPI.Application.Users.Commands.UpdateUser;
using CSHSBackendAPI.Application.Users.DTOs;
using CSHSBackendAPI.Application.Users.Queries.GetAllUsers;
using CSHSBackendAPI.Application.Users.Queries.GetCurrentUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly GetAllUsersQueryHandler _getAllHandler;
    private readonly GetCurrentUserQueryHandler _getCurrentHandler;
    private readonly CreateUserCommandHandler _createHandler;
    private readonly UpdateUserCommandHandler _updateHandler;
    private readonly ResetPasswordCommandHandler _resetPasswordHandler;

    public UsersController(
        GetAllUsersQueryHandler getAllHandler,
        GetCurrentUserQueryHandler getCurrentHandler,
        CreateUserCommandHandler createHandler,
        UpdateUserCommandHandler updateHandler,
        ResetPasswordCommandHandler resetPasswordHandler)
    {
        _getAllHandler = getAllHandler;
        _getCurrentHandler = getCurrentHandler;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _resetPasswordHandler = resetPasswordHandler;
    }

    // GET /api/users
    [HttpGet]
    [Authorize(Roles = "technical_admin,admin")]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? role,
        [FromQuery] long? campus_id)
    {
        var result = await _getAllHandler.Handle(role, campus_id);
        return Ok(ApiResponse<IEnumerable<UserDto>>.Ok(result));
    }

    // GET /api/users/me
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var userId = long.Parse(User.FindFirst("sub")!.Value);
        var result = await _getCurrentHandler.Handle(userId);
        return Ok(ApiResponse<UserDto>.Ok(result));
    }

    // POST /api/users
    [HttpPost]
    [Authorize(Roles = "technical_admin")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var result = await _createHandler.Handle(request);
        return Ok(ApiResponse<UserDto>.Ok(result, "User created successfully."));
    }

    // PATCH /api/users/:id
    [HttpPatch("{id}")]
    [Authorize(Roles = "technical_admin")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateUserRequest request)
    {
        var result = await _updateHandler.Handle(id, request);
        return Ok(ApiResponse<UserDto>.Ok(result, "User updated successfully."));
    }

    // PATCH /api/users/:id/password
    [HttpPatch("{id}/password")]
    [Authorize(Roles = "technical_admin")]
    public async Task<IActionResult> ResetPassword(long id, [FromBody] ResetPasswordRequest request)
    {
        await _resetPasswordHandler.Handle(id, request);
        return Ok(ApiResponse<object>.Ok("Password reset successfully."));
    }
}