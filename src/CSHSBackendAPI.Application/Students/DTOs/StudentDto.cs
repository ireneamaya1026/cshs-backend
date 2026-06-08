namespace CSHSBackendAPI.Application.Students.DTOs;

public class StudentDto
{
    public long Id { get; set; }
    public string StudentId { get; set; } = string.Empty;
    public long CampusId { get; set; }
    public string CampusName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? Suffix { get; set; }
    public DateOnly? Birthdate { get; set; }
    public string? Sex { get; set; }
    public string? ContactNumber { get; set; }
    public string? Address { get; set; }
    public string GradeLevel { get; set; } = string.Empty;
    public string? Section { get; set; }
    public string Department { get; set; } = string.Empty;
    public string? Program { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string? Lrn { get; set; }
    public decimal Balance { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}