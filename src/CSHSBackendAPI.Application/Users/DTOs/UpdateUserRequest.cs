namespace CSHSBackendAPI.Application.Users.DTOs;

public class UpdateUserRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public bool? IsActive { get; set; }
    public long? CampusId { get; set; }
}