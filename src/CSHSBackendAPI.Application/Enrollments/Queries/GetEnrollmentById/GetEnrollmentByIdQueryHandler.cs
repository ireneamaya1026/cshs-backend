using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Enrollments.DTOs;

namespace CSHSBackendAPI.Application.Enrollments.Queries.GetEnrollmentById;

public class GetEnrollmentByIdQueryHandler
{
    private readonly IEnrollmentRepository _enrollmentRepo;

    public GetEnrollmentByIdQueryHandler(IEnrollmentRepository enrollmentRepo) =>
        _enrollmentRepo = enrollmentRepo;

    public async Task<EnrollmentDto> Handle(long id)
    {
        var e = await _enrollmentRepo.GetByIdAsync(id)
            ?? throw new NotFoundException("Enrollment", id);

        return new EnrollmentDto
        {
            Id = e.Id,
            ReferenceNo = e.ReferenceNo,
            CampusId = e.CampusId,
            CampusName = e.Campus?.Name ?? string.Empty,
            StudentId = e.StudentId,
            SchoolYear = e.SchoolYear,
            Department = e.Department.ToString(),
            Source = e.Source.ToString(),
            FirstName = e.FirstName,
            MiddleName = e.MiddleName,
            LastName = e.LastName,
            Suffix = e.Suffix,
            Birthdate = e.Birthdate,
            Sex = e.Sex?.ToString(),
            ContactNumber = e.ContactNumber,
            Email = e.Email,
            Address = e.Address,
            GradeLevel = e.GradeLevel,
            Section = e.Section,
            Program = e.Program,
            StudentType = e.StudentType.ToString(),
            Lrn = e.Lrn,
            WorkflowId = e.WorkflowId,
            CurrentStep = e.CurrentStep,
            PreviousStep = e.PreviousStep,
            AssessedFees = e.AssessedFees,
            NetFee = e.NetFee,
            AmountPaid = e.AmountPaid,
            Balance = e.Balance,
            HasMissingDocs = e.HasMissingDocs,
            ConvertedToStudent = e.ConvertedToStudent,
            SubmittedAt = e.SubmittedAt,
            CreatedAt = e.CreatedAt,
            StageHistories = e.StageHistories?.Select(h => new EnrollmentStageHistoryDto
            {
                Id = h.Id,
                Step = h.Step,
                FromStep = h.FromStep,
                ActionId = h.ActionId,
                ByName = h.ByName,
                ByRole = h.ByRole,
                Note = h.Note,
                RecordedAt = h.RecordedAt
            }).ToList()
        };
    }
}