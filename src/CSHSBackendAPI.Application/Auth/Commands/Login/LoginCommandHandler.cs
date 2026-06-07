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

   // Update LoginCommandHandler.cs
    public async Task<LoginResponse> Handle(LoginCommand command)
    {
        var user = await _authRepo.GetByEmailAsync(command.Email, string.Empty)
            ?? throw new UnauthorizedException("Invalid email or password.");

        if (!user.IsActive)
            throw new UnauthorizedException("Your account has been deactivated. Contact your System Admin.");

        bool isValid = false;

        // Check if password is already BCrypt
        if (user.PasswordHash.StartsWith("$2a$") || user.PasswordHash.StartsWith("$2b$"))
        {
            // Already BCrypt — verify normally
            isValid = BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash);
        }
        else
        {
            // Legacy SHA-256 hash — verify then migrate to BCrypt
            var sha256Hash = ComputeSha256(command.Password);
            if (sha256Hash == user.PasswordHash)
            {
                isValid = true;
                // Migrate to BCrypt silently
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);
                await _authRepo.UpdateAsync(user);
            }
        }

        if (!isValid)
            throw new UnauthorizedException("Invalid email or password.");

        user.LastLoginAt = DateTime.UtcNow;
        await _authRepo.UpdateAsync(user);

        var token = _jwtService.GenerateToken(user);

        return new LoginResponse
        {
            Token = token,
            User = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString(),
                Campus = user.Campus == null ? null : new CampusResponse
                {
                    Id = user.Campus.Id,
                    Name = user.Campus.Name,
                    CampusKey = user.Campus.CampusKey
                }
            }
        };
    }

    // SHA-256 helper
    private static string ComputeSha256(string input)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hash).ToLower();
    }
}