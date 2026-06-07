using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Users.DTOs;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler
{
    private readonly IUserRepository _userRepo;

    public CreateUserCommandHandler(IUserRepository userRepo) =>
        _userRepo = userRepo;

    public async Task<UserDto> Handle(CreateUserRequest request)
    {
        // Check email is unique
        if (await _userRepo.EmailExistsAsync(request.Email))
            throw new ValidationException(new List<string>
            {
                $"Email '{request.Email}' is already in use."
            });

        // Validate role
        if (!Enum.TryParse<UserRole>(request.Role, true, out var role))
            throw new ValidationException(new List<string>
            {
                $"Invalid role '{request.Role}'."
            });

        var user = new SystemUser
        {
            Name = request.Name,
            Email = request.Email.ToLower().Trim(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = role,
            CampusId = request.CampusId,
            IsActive = true
        };

        var created = await _userRepo.CreateAsync(user);

        return new UserDto
        {
            Id = created.Id,
            Name = created.Name,
            Email = created.Email,
            Role = created.Role.ToString(),
            IsActive = created.IsActive,
            CampusId = created.CampusId,
            CreatedAt = created.CreatedAt
        };
    }
}