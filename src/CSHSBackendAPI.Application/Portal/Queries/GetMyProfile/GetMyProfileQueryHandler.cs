using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Portal.DTOs;

namespace CSHSBackendAPI.Application.Portal.Queries.GetMyProfile;

public class GetMyProfileQueryHandler
{
    private readonly IPortalRepository _repo;

    public GetMyProfileQueryHandler(IPortalRepository repo) =>
        _repo = repo;

    public async Task<MyProfileDto> Handle(long studentId)
    {
        var student = await _repo.GetStudentByIdAsync(studentId)
            ?? throw new NotFoundException("Student", studentId);

        return new MyProfileDto
        {
            Id = student.Id,
            StudentId = student.StudentId,
            FirstName = student.FirstName,
            MiddleName = student.MiddleName,
            LastName = student.LastName,
            GradeLevel = student.GradeLevel,
            Section = student.Section,
            SchoolYear = student.SchoolYear,
            Balance = student.Balance,
            CampusName = student.Campus?.Name ?? string.Empty
        };
    }
}