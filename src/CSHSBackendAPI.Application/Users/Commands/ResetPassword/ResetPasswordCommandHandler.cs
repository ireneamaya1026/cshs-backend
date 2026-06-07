using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Users.DTOs;

namespace CSHSBackendAPI.Application.Users.Commands.ResetPassword;

public class ResetPasswordCommandHandler
{
    private readonly IUserRepository _userRepo;

    public ResetPasswordCommandHandler(IUserRepository userRepo) =>
        _userRepo = userRepo;

    public async Task Handle(long id, ResetPasswordRequest request)
    {
        var user = await _userRepo.GetByIdAsync(id)
            ?? throw new NotFoundException("User", id);

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        await _userRepo.UpdateAsync(user);
    }
}