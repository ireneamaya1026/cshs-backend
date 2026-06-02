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
        var user = await _authRepo.GetByEmailAsync(command.Email, string.Empty)
            ?? throw new UnauthorizedException("Invalid email or password.");

        if (!user.IsActive)
            throw new UnauthorizedException("Your account has been deactivated.");

        if (!BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid email or password.");

        var token = _jwtService.GenerateToken(user);

        return new LoginResponse
        {
            Token = token,
            Role = user.Role.ToString(),
            FullName = user.Name,
            SchoolSlug = string.Empty,
            CampusId = (int?)user.CampusId,
            ExpiresAt = DateTime.UtcNow.AddHours(8)
        };
    }
}