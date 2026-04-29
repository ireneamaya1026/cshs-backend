using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Common.Interfaces.Services;
using CSHSBackendAPI.Infrastructure.Data;
using CSHSBackendAPI.Infrastructure.Repositories;
using CSHSBackendAPI.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSHSBackendAPI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Tenant service must be registered first
        // Scoped = one instance per HTTP request
        services.AddScoped<ITenantService, TenantService>();

        // DbContext uses tenant connection string per request
        services.AddDbContext<AppDbContext>((provider, options) =>
        {
            var tenantService = provider.GetRequiredService<ITenantService>();

            // SuperAdmin requests use a default master connection
            var connectionString = configuration.GetConnectionString("master")!;

            try
            {
                connectionString = tenantService.GetConnectionString();
            }
            catch { /* no tenant set yet, use master */ }

            options.UseSqlServer(connectionString);
        });

        // Repositories
        services.AddScoped<ISchoolRepository, SchoolRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();

        // Services
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}