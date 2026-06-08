using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Students.DTOs;

namespace CSHSBackendAPI.Application.Students.Queries.GetStudentById;

public class GetStudentByIdQueryHandler
{
    private readonly IStudentRepository _studentRepo;

    public GetStudentByIdQueryHandler(IStudentRepository studentRepo) =>
        _studentRepo = studentRepo;

    public async Task<StudentDto> Handle(long id)
    {
        var s = await _studentRepo.GetByIdAsync(id)
            ?? throw new NotFoundException("Student", id);

        return new StudentDto
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
        };
    }
}