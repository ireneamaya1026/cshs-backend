namespace CSHSBackendAPI.Application.Common.Interfaces.Services;

public interface ITenantService
{
    string GetCurrentSchoolSlug();
    void SetCurrentTenant(string schoolSlug);
    string GetConnectionString();
}