namespace CSHSBackendAPI.Application.Students.DTOs;

public class UpdateStudentRequest
{
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Suffix { get; set; }
    public DateOnly? Birthdate { get; set; }
    public string? Sex { get; set; }
    public string? ContactNumber { get; set; }
    public string? Address { get; set; }
    public string? GradeLevel { get; set; }
    public string? Section { get; set; }
    public string? Program { get; set; }
    public string? Lrn { get; set; }
    public string? Status { get; set; }
}