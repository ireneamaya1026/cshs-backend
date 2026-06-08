using System.Text;
using CSHSBackendAPI.API.Middleware;
using CSHSBackendAPI.Application.Auth.Commands.Login;
using CSHSBackendAPI.Application.Auth.Commands.RefreshToken;
using CSHSBackendAPI.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CSHSBackendAPI.Application.Config.Queries.GetConfig;
using CSHSBackendAPI.Application.Config.Commands.UpdateConfig;

var builder = WebApplication.CreateBuilder(args);

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure (DB, Repos, Services)
builder.Services.AddInfrastructure(builder.Configuration);

// Application Handlers
builder.Services.AddScoped<LoginCommandHandler>();

// Add this line
builder.Services.AddScoped<RefreshTokenCommandHandler>();

// JWT Authentication
// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Disable auto-mapping of claim types
        options.MapInboundClaims = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                
            RoleClaimType = "role",
            NameClaimType = "name"
        };
    });

// Add handlers
builder.Services.AddScoped<CSHSBackendAPI.Application.Config.Queries.GetConfig.GetConfigQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Config.Commands.UpdateConfig.UpdateConfigCommandHandler>();

builder.Services.AddScoped<CSHSBackendAPI.Application.Campuses.Queries.GetAllCampuses.GetAllCampusesQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Campuses.Commands.CreateCampus.CreateCampusCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Campuses.Commands.UpdateCampus.UpdateCampusCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Campuses.Commands.DeleteCampus.DeleteCampusCommandHandler>();

builder.Services.AddScoped<CSHSBackendAPI.Application.SchoolYears.Queries.GetAllSchoolYears.GetAllSchoolYearsQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.SchoolYears.Commands.CreateSchoolYear.CreateSchoolYearCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.SchoolYears.Commands.UpdateSchoolYear.UpdateSchoolYearCommandHandler>();

builder.Services.AddScoped<CSHSBackendAPI.Application.Users.Queries.GetAllUsers.GetAllUsersQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Users.Queries.GetCurrentUser.GetCurrentUserQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Users.Commands.CreateUser.CreateUserCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Users.Commands.UpdateUser.UpdateUserCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Users.Commands.ResetPassword.ResetPasswordCommandHandler>();

builder.Services.AddScoped<CSHSBackendAPI.Application.Enrollments.Queries.GetAllEnrollments.GetAllEnrollmentsQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Enrollments.Queries.GetEnrollmentById.GetEnrollmentByIdQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Enrollments.Commands.CreateEnrollment.CreateEnrollmentCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Enrollments.Commands.AdvanceEnrollment.AdvanceEnrollmentCommandHandler>();

builder.Services.AddScoped<CSHSBackendAPI.Application.Students.Queries.GetAllStudents.GetAllStudentsQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Students.Queries.GetStudentById.GetStudentByIdQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Students.Queries.GetStudentGrades.GetStudentGradesQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Students.Queries.GetStudentAttendance.GetStudentAttendanceQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Students.Commands.UpdateStudent.UpdateStudentCommandHandler>();

builder.Services.AddAuthorization();



// CORS for React Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
        policy.WithOrigins(
                "http://localhost:5173",
                "http://localhost:3000",
                "http://localhost:4200"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();