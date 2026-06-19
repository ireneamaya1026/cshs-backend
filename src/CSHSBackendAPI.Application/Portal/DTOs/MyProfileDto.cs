namespace CSHSBackendAPI.Application.Portal.DTOs;

public class MyProfileDto
{
    public long Id { get; set; }
    public string StudentId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string GradeLevel { get; set; } = string.Empty;
    public string? Section { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string CampusName { get; set; } = string.Empty;
}