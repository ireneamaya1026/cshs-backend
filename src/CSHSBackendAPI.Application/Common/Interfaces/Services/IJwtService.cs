using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(User user);
    string? GetSchoolSlugFromToken(string token);
    bool ValidateToken(string token);
}