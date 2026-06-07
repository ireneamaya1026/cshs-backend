using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Users.DTOs;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler
{
    private readonly IUserRepository _userRepo;

    public UpdateUserCommandHandler(IUserRepository userRepo) =>
        _userRepo = userRepo;

    public async Task<UserDto> Handle(long id, UpdateUserRequest request)
    {
        var user = await _userRepo.GetByIdAsync(id)
            ?? throw new NotFoundException("User", id);

        if (request.Name != null) user.Name = request.Name;
        if (request.Email != null) user.Email = request.Email.ToLower().Trim();
        if (request.IsActive.HasValue) user.IsActive = request.IsActive.Value;
        if (request.CampusId.HasValue) user.CampusId = request.CampusId.Value;

        if (request.Role != null)
        {
            if (!Enum.TryParse<UserRole>(request.Role, true, out var role))
                throw new ValidationException(new List<string>
                {
                    $"Invalid role '{request.Role}'."
                });
            user.Role = role;
        }

        await _userRepo.UpdateAsync(user);

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