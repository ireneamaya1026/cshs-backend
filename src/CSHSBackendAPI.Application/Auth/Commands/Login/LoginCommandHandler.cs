using CSHSBackendAPI.Application.Auth.DTOs;
using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Common.Interfaces.Services;

namespace CSHSBackendAPI.Application.Auth.Commands.Login;

public class LoginCommandHandler
{
    private readonly IAuthRepository _authRepo;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(
        IAuthRepository authRepo,
        IJwtService jwtService)
    {
        _authRepo = authRepo;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Handle(LoginCommand command)
    {
        // 1. Find user by email and school
        var user = await _authRepo.GetByEmailAsync(command.Email, command.SchoolSlug)
            ?? throw new UnauthorizedException("Invalid email or password.");

        // 2. Check if account is active
        if (!user.IsActive)
            throw new UnauthorizedException("Your account has been deactivated.");

        // 3. Debug - temporary
        var isValid = BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash);
        Console.WriteLine($"Password: {command.Password}");
        Console.WriteLine($"Hash: {user.PasswordHash}");
        Console.WriteLine($"Valid: {isValid}");

        // 4. Verify password
        if (!isValid)
            throw new UnauthorizedException("Invalid email or password.");

        // 5. Generate JWT token
        var token = _jwtService.GenerateToken(user);

        return new LoginResponse
        {
            Token = token,
            Role = user.Role.ToString(),
            FullName = $"{user.FirstName} {user.LastName}",
            SchoolSlug = user.SchoolSlug,
            CampusId = user.CampusId,
            ExpiresAt = DateTime.UtcNow.AddHours(8)
        };
    }
}