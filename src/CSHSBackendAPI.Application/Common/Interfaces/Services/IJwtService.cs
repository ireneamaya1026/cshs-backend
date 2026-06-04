using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(SystemUser user);
    string? GetSchoolSlugFromToken(string token);
    long? GetUserIdFromToken(string token);
    bool ValidateToken(string token);
}