using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Enrollments.DTOs;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Enrollments.Commands.CreateEnrollment;

public class CreateEnrollmentCommandHandler
{
    private readonly IEnrollmentRepository _enrollmentRepo;

    public CreateEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepo) =>
        _enrollmentRepo = enrollmentRepo;

    public async Task<EnrollmentDto> Handle(CreateEnrollmentRequest request, string byName, string byRole)
    {
        if (!Enum.TryParse<Department>(request.Department, true, out var dept))
            throw new ValidationException(new List<string> { $"Invalid department '{request.Department}'." });

        if (!Enum.TryParse<EnrollmentSource>(request.Source, true, out var source))
            throw new ValidationException(new List<string> { $"Invalid source '{request.Source}'." });

        if (!Enum.TryParse<StudentType>(request.StudentType, true, out var studentType))
            throw new ValidationException(new List<string> { $"Invalid student type '{request.StudentType}'." });

        var referenceNo = await _enrollmentRepo.GenerateReferenceNoAsync(request.SchoolYear);

        var enrollment = new Enrollment
        {
            CampusId = request.CampusId,
            ReferenceNo = referenceNo,
            SchoolYear = request.SchoolYear,
            Department = dept,
            Source = source,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            Suffix = request.Suffix,
            Birthdate = request.Birthdate,
            ContactNumber = request.ContactNumber,
            Email = request.Email,
            Address = request.Address,
            GradeLevel = request.GradeLevel,
            Section = request.Section,
            Program = request.Program,
            StudentType = studentType,
            Lrn = request.Lrn,
            WorkflowId = request.WorkflowId,
            WorkflowVersion = 1,
            CurrentStep = "pre_registered",
            SubmittedAt = DateTime.UtcNow
        };

        if (request.Sex != null && Enum.TryParse<Sex>(request.Sex, true, out var sex))
            enrollment.Sex = sex;

        var created = await _enrollmentRepo.CreateAsync(enrollment);

        // Append initial stage history
        await _enrollmentRepo.AddStageHistoryAsync(new EnrollmentStageHistory
        {
            EnrollmentId = created.Id,
            Step = "pre_registered",
            FromStep = null,
            ActionId = "create",
            ByName = byName,
            ByRole = byRole,
            Note = "Enrollment created.",
            RecordedAt = DateTime.UtcNow
        });

        return new EnrollmentDto
        {
            Id = created.Id,
            ReferenceNo = created.ReferenceNo,
            CampusId = created.CampusId,
            SchoolYear = created.SchoolYear,
            Department = created.Department.ToString(),
            Source = created.Source.ToString(),
            FirstName = created.FirstName,
            LastName = created.LastName,
            GradeLevel = created.GradeLevel,
            StudentType = created.StudentType.ToString(),
            WorkflowId = created.WorkflowId,
            CurrentStep = created.CurrentStep,
            SubmittedAt = created.SubmittedAt,
            CreatedAt = created.CreatedAt
        };
    }
}