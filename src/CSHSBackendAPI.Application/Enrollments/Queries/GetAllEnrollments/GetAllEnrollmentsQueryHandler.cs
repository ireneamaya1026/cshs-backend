using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Enrollments.DTOs;

namespace CSHSBackendAPI.Application.Enrollments.Queries.GetAllEnrollments;

public class GetAllEnrollmentsQueryHandler
{
    private readonly IEnrollmentRepository _enrollmentRepo;

    public GetAllEnrollmentsQueryHandler(IEnrollmentRepository enrollmentRepo) =>
        _enrollmentRepo = enrollmentRepo;

    public async Task<object> Handle(
        string? schoolYear, string? status,
        long? campusId, string? dept,
        int page = 1, int limit = 20)
    {
        var (items, total) = await _enrollmentRepo.GetAllAsync(
            schoolYear, status, campusId, dept, page, limit);

        var data = items.Select(e => new EnrollmentDto
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
            CreatedAt = e.CreatedAt
        });

        return new { data, total, page, limit };
    }
}