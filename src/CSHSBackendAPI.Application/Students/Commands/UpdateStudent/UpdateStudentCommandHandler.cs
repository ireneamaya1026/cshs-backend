using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Students.DTOs;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Students.Commands.UpdateStudent;

public class UpdateStudentCommandHandler
{
    private readonly IStudentRepository _studentRepo;

    public UpdateStudentCommandHandler(IStudentRepository studentRepo) =>
        _studentRepo = studentRepo;

    public async Task<StudentDto> Handle(long id, UpdateStudentRequest request)
    {
        var student = await _studentRepo.GetByIdAsync(id)
            ?? throw new NotFoundException("Student", id);

        if (request.FirstName != null) student.FirstName = request.FirstName;
        if (request.MiddleName != null) student.MiddleName = request.MiddleName;
        if (request.LastName != null) student.LastName = request.LastName;
        if (request.Suffix != null) student.Suffix = request.Suffix;
        if (request.Birthdate.HasValue) student.Birthdate = request.Birthdate;
        if (request.ContactNumber != null) student.ContactNumber = request.ContactNumber;
        if (request.Address != null) student.Address = request.Address;
        if (request.GradeLevel != null) student.GradeLevel = request.GradeLevel;
        if (request.Section != null) student.Section = request.Section;
        if (request.Program != null) student.Program = request.Program;
        if (request.Lrn != null) student.Lrn = request.Lrn;

        if (request.Sex != null &&
            Enum.TryParse<Sex>(request.Sex, true, out var sex))
            student.Sex = sex;

        if (request.Status != null &&
            Enum.TryParse<StudentStatus>(request.Status, true, out var status))
            student.Status = status;

        await _studentRepo.UpdateAsync(student);

        return new StudentDto
        {
            Id = student.Id,
            StudentId = student.StudentId,
            CampusId = student.CampusId,
            CampusName = student.Campus?.Name ?? string.Empty,
            FirstName = student.FirstName,
            MiddleName = student.MiddleName,
            LastName = student.LastName,
            Suffix = student.Suffix,
            Birthdate = student.Birthdate,
            Sex = student.Sex?.ToString(),
            ContactNumber = student.ContactNumber,
            Address = student.Address,
            GradeLevel = student.GradeLevel,
            Section = student.Section,
            Department = student.Department.ToString(),
            Program = student.Program,
            SchoolYear = student.SchoolYear,
            Lrn = student.Lrn,
            Balance = student.Balance,
            Status = student.Status.ToString(),
            CreatedAt = student.CreatedAt
        };
    }
}