using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Students.DTOs;

namespace CSHSBackendAPI.Application.Students.Queries.GetAllStudents;

public class GetAllStudentsQueryHandler
{
    private readonly IStudentRepository _studentRepo;

    public GetAllStudentsQueryHandler(IStudentRepository studentRepo) =>
        _studentRepo = studentRepo;

    public async Task<object> Handle(
        string? gradeLevel, string? section,
        long? campusId, string? schoolYear,
        string? status, int page = 1, int limit = 20)
    {
        var (items, total) = await _studentRepo.GetAllAsync(
            gradeLevel, section, campusId, schoolYear, status, page, limit);

        var data = items.Select(s => new StudentDto
        {
            Id = s.Id,
            StudentId = s.StudentId,
            CampusId = s.CampusId,
            CampusName = s.Campus?.Name ?? string.Empty,
            FirstName = s.FirstName,
            MiddleName = s.MiddleName,
            LastName = s.LastName,
            Suffix = s.Suffix,
            Birthdate = s.Birthdate,
            Sex = s.Sex?.ToString(),
            ContactNumber = s.ContactNumber,
            Address = s.Address,
            GradeLevel = s.GradeLevel,
            Section = s.Section,
            Department = s.Department.ToString(),
            Program = s.Program,
            SchoolYear = s.SchoolYear,
            Lrn = s.Lrn,
            Balance = s.Balance,
            Status = s.Status.ToString(),
            CreatedAt = s.CreatedAt
        });

        return new { data, total, page, limit };
    }
}