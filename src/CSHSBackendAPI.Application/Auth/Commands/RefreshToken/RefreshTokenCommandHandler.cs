using CSHSBackendAPI.Application.Auth.DTOs;
using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Common.Interfaces.Services;

namespace CSHSBackendAPI.Application.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler
{
    private readonly IAuthRepository _authRepo;
    private readonly IJwtService _jwtService;

    public RefreshTokenCommandHandler(
        IAuthRepository authRepo,
        IJwtService jwtService)
    {
        _authRepo = authRepo;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Handle(RefreshTokenRequest request)
    {
        // Validate existing token
        if (!_jwtService.ValidateToken(request.Token))
            throw new UnauthorizedException("Invalid or expired token.");

        // Get user id from token
        var userId = _jwtService.GetUserIdFromToken(request.Token);
        if (userId == null)
            throw new UnauthorizedException("Invalid token.");

        var user = await _authRepo.GetByIdAsync((int)userId)
            ?? throw new UnauthorizedException("User not found.");

        if (!user.IsActive)
            throw new UnauthorizedException("Account has been deactivated.");

        // Generate new token
        var newToken = _jwtService.GenerateToken(user);

        return new LoginResponse
        {
            Token = newToken,
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
}