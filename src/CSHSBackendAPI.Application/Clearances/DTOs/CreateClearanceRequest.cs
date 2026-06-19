namespace CSHSBackendAPI.Application.Clearances.DTOs;

public class CreateClearanceRequest
{
    public long StudentId { get; set; }
    public long CampusId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string? Reason { get; set; }
}