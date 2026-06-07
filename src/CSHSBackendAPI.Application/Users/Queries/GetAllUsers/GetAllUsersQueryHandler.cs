using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Users.DTOs;

namespace CSHSBackendAPI.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler
{
    private readonly IUserRepository _userRepo;

    public GetAllUsersQueryHandler(IUserRepository userRepo) =>
        _userRepo = userRepo;

    public async Task<IEnumerable<UserDto>> Handle(string? role, long? campusId)
    {
        var users = await _userRepo.GetAllAsync(role, campusId);

        return users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Role = u.Role.ToString(),
            IsActive = u.IsActive,
            CampusId = u.CampusId,
            CampusName = u.Campus?.Name,
            CampusKey = u.Campus?.CampusKey,
            LastLoginAt = u.LastLoginAt,
            CreatedAt = u.CreatedAt
        });
    }
}