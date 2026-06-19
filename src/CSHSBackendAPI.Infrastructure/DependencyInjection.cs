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
        // Single connection string — one DB per school deployment
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<ISchoolConfigRepository, SchoolConfigRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();

        // Services
        services.AddScoped<IJwtService, JwtService>();

        // Add to repositories section
        services.AddScoped<ISchoolConfigRepository, SchoolConfigRepository>();  

        services.AddScoped<ICampusRepository, CampusRepository>();

        services.AddScoped<ISchoolYearRepository, SchoolYearRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IWorkflowRepository, WorkflowRepository>();

        services.AddScoped<IStudentRepository, StudentRepository>();

        services.AddScoped<IGradeRepository, GradeRepository>();

        services.AddScoped<IGradeChangeRepository, GradeChangeRepository>();

        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        
        services.AddScoped<ISubjectLoadRepository, SubjectLoadRepository>();

        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IClearanceRepository, ClearanceRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IFeeRepository, FeeRepository>();
        services.AddScoped<IWorkflowAuditRepository, WorkflowAuditRepository>();

        return services;
    }
}