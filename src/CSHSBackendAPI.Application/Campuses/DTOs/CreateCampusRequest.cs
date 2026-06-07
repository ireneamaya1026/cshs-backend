namespace CSHSBackendAPI.Application.Campuses.DTOs;

public class CreateCampusRequest
{
    public string CampusKey { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? ShortName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool HasBasicEd { get; set; } = true;
    public bool HasCollege { get; set; } = false;
    public string? CollegeProgramsJson { get; set; }
    public short SortOrder { get; set; } = 0;
}