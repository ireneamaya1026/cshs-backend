using CSHSBackendAPI.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace CSHSBackendAPI.Infrastructure.Services;

public class TenantService : ITenantService
{
    private string _currentSchoolSlug = string.Empty;
    private readonly IConfiguration _config;

    public TenantService(IConfiguration config) => _config = config;

    public void SetCurrentTenant(string schoolSlug) =>
        _currentSchoolSlug = schoolSlug;

    public string GetCurrentSchoolSlug() => _currentSchoolSlug;

    public string GetConnectionString()
    {
        if (string.IsNullOrEmpty(_currentSchoolSlug))
            throw new Exception("No school tenant has been set for this request.");

        return _config.GetConnectionString(_currentSchoolSlug)
            ?? throw new Exception($"No database configured for school: {_currentSchoolSlug}");
    }
}