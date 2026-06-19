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

builder.Services.AddScoped<CSHSBackendAPI.Application.Grades.Queries.GetBasicEdGrades.GetBasicEdGradesQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Grades.Queries.GetCollegeGrades.GetCollegeGradesQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Grades.Queries.GetActivities.GetActivitiesQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Grades.Commands.UpsertBasicEdGrade.UpsertBasicEdGradeCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Grades.Commands.UpsertCollegeGrade.UpsertCollegeGradeCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Grades.Commands.SubmitGrade.SubmitGradeCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Grades.Commands.ApproveGrade.ApproveGradeCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Grades.Commands.PostGrade.PostGradeCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Grades.Commands.RejectGrade.RejectGradeCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Grades.Commands.CreateActivity.CreateActivityCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Grades.Commands.SaveScores.SaveScoresCommandHandler>();

// Grade Changes
builder.Services.AddScoped<CSHSBackendAPI.Application.GradeChanges.Queries.GetGradeChanges.GetGradeChangesQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.GradeChanges.Commands.SubmitGradeChange.SubmitGradeChangeCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.GradeChanges.Commands.ApproveGradeChange.ApproveGradeChangeCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.GradeChanges.Commands.PostGradeChange.PostGradeChangeCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.GradeChanges.Commands.RejectGradeChange.RejectGradeChangeCommandHandler>();

// Attendance
builder.Services.AddScoped<CSHSBackendAPI.Application.Attendance.Queries.GetAttendance.GetAttendanceQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Attendance.Queries.GetAttendanceSummary.GetAttendanceSummaryQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Attendance.Commands.SaveBatchAttendance.SaveBatchAttendanceCommandHandler>();

// Subject Loads
builder.Services.AddScoped<CSHSBackendAPI.Application.SubjectLoads.Queries.GetSubjectLoads.GetSubjectLoadsQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.SubjectLoads.Commands.CreateSubjectLoad.CreateSubjectLoadCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.SubjectLoads.Commands.UpdateSubjectLoad.UpdateSubjectLoadCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.SubjectLoads.Commands.DeleteSubjectLoad.DeleteSubjectLoadCommandHandler>();

// Documents
builder.Services.AddScoped<CSHSBackendAPI.Application.Documents.Queries.GetDocuments.GetDocumentsQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Documents.Commands.CreateDocument.CreateDocumentCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Documents.Commands.AdvanceDocument.AdvanceDocumentCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Documents.Commands.ReleaseDocument.ReleaseDocumentCommandHandler>();

// Clearances
builder.Services.AddScoped<CSHSBackendAPI.Application.Clearances.Queries.GetClearances.GetClearancesQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Clearances.Commands.CreateClearance.CreateClearanceCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Clearances.Commands.SignClearance.SignClearanceCommandHandler>();

// Payments
builder.Services.AddScoped<CSHSBackendAPI.Application.Payments.Queries.GetPayments.GetPaymentsQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Payments.Queries.GetPaymentSummary.GetPaymentSummaryQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Payments.Commands.RecordPayment.RecordPaymentCommandHandler>();

// Fees
builder.Services.AddScoped<CSHSBackendAPI.Application.Fees.Queries.GetFees.GetFeesQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Fees.Commands.CreateFee.CreateFeeCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Fees.Commands.UpdateFee.UpdateFeeCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Fees.Commands.DeleteFee.DeleteFeeCommandHandler>();

// Workflows
builder.Services.AddScoped<CSHSBackendAPI.Application.Workflows.Queries.GetWorkflows.GetWorkflowsQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Workflows.Commands.UpdateWorkflow.UpdateWorkflowCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Workflows.Commands.LogWorkflowAudit.LogWorkflowAuditCommandHandler>();

// Announcements
builder.Services.AddScoped<CSHSBackendAPI.Application.Announcements.Queries.GetAnnouncements.GetAnnouncementsQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Announcements.Commands.CreateAnnouncement.CreateAnnouncementCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Announcements.Commands.UpdateAnnouncement.UpdateAnnouncementCommandHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Announcements.Commands.DeleteAnnouncement.DeleteAnnouncementCommandHandler>();

// Form Templates
builder.Services.AddScoped<CSHSBackendAPI.Application.FormTemplates.Queries.GetFormTemplates.GetFormTemplatesQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.FormTemplates.Commands.UpsertFormTemplate.UpsertFormTemplateCommandHandler>();

// Portal
builder.Services.AddScoped<CSHSBackendAPI.Application.Portal.Queries.GetMyProfile.GetMyProfileQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Portal.Queries.GetMyGrades.GetMyGradesQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Portal.Queries.GetMyAttendance.GetMyAttendanceQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Portal.Queries.GetMySchedule.GetMyScheduleQueryHandler>();
builder.Services.AddScoped<CSHSBackendAPI.Application.Portal.Queries.GetMyAnnouncements.GetMyAnnouncementsQueryHandler>();

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