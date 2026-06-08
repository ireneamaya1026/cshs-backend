namespace CSHSBackendAPI.Application.Enrollments.DTOs;

public class AdvanceEnrollmentRequest
{
    public string ActionId { get; set; } = string.Empty;
    public string? Note { get; set; }
}