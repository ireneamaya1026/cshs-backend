using CSHSBackendAPI.Application.Common.Interfaces.Services;

namespace CSHSBackendAPI.API.Middleware;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context, ITenantService tenantService)
    {
        // Extract schoolSlug from JWT claims
        var schoolSlug = context.User.FindFirst("schoolSlug")?.Value;

        if (!string.IsNullOrEmpty(schoolSlug))
            tenantService.SetCurrentTenant(schoolSlug);

        await _next(context);
    }
}