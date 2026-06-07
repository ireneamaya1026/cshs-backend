using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Users.DTOs;

namespace CSHSBackendAPI.Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler
{
    private readonly IUserRepository _userRepo;

    public GetCurrentUserQueryHandler(IUserRepository userRepo) =>
        _userRepo = userRepo;

    public async Task<UserDto> Handle(long userId)
    {
        var user = await _userRepo.GetByIdAsync(userId)
            ?? throw new NotFoundException("User", userId);

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString(),
            IsActive = user.IsActive,
            CampusId = user.CampusId,
            CampusName = user.Campus?.Name,
            CampusKey = user.Campus?.CampusKey,
            LastLoginAt = user.LastLoginAt,
            CreatedAt = user.CreatedAt
        };
    }
}