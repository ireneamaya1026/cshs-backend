namespace CSHSBackendAPI.Application.Enrollments.DTOs;

public class CreateEnrollmentRequest
{
    public long CampusId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string Department { get; set; } = "basicEd";
    public string Source { get; set; } = "admin";
    // Student info
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? Suffix { get; set; }
    public DateOnly? Birthdate { get; set; }
    public string? Sex { get; set; }
    public string? ContactNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    // Academic
    public string GradeLevel { get; set; } = string.Empty;
    public string? Section { get; set; }
    public string? Program { get; set; }
    public string StudentType { get; set; } = "new";
    public string? Lrn { get; set; }
    // Workflow
    public string WorkflowId { get; set; } = string.Empty;
}