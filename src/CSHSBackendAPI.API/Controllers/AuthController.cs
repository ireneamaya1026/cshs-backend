using CSHSBackendAPI.Application.Auth.Commands.Login;
using CSHSBackendAPI.Application.Auth.Commands.RefreshToken;
using CSHSBackendAPI.Application.Auth.DTOs;
using CSHSBackendAPI.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly LoginCommandHandler _loginHandler;
    private readonly RefreshTokenCommandHandler _refreshHandler;

    public AuthController(
        LoginCommandHandler loginHandler,
        RefreshTokenCommandHandler refreshHandler)
    {
        _loginHandler = loginHandler;
        _refreshHandler = refreshHandler;
    }

    // POST /api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var command = new LoginCommand
        {
            Email = request.Email,
            Password = request.Password
        };

        var result = await _loginHandler.Handle(command);
        return Ok(ApiResponse<LoginResponse>.Ok(result, "Login successful."));
    }

    // POST /api/auth/refresh
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        var result = await _refreshHandler.Handle(request);
        return Ok(ApiResponse<LoginResponse>.Ok(result, "Token refreshed."));
    }

    // POST /api/auth/logout
    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        // JWT is stateless — client just discards the token
        // If you want server-side blacklisting, add it here later
        return Ok(ApiResponse<object>.Ok("Logged out successfully."));
    }

    // Temporary — remove after testing
    [HttpGet("hash/{password}")]
    public IActionResult GetHash(string password)
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(password);
        return Ok(hash);
    }
}