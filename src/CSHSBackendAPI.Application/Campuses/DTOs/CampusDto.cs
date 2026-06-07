namespace CSHSBackendAPI.Application.Campuses.DTOs;

public class CampusDto
{
    public long Id { get; set; }
    public string CampusKey { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? ShortName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool HasBasicEd { get; set; }
    public bool HasCollege { get; set; }
    public string? CollegeProgramsJson { get; set; }
    public bool IsActive { get; set; }
    public short SortOrder { get; set; }
}