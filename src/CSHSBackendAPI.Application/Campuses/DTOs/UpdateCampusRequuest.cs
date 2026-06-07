namespace CSHSBackendAPI.Application.Campuses.DTOs;

public class UpdateCampusRequest
{
    public string? CampusKey { get; set; }
    public string? Name { get; set; }
    public string? ShortName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool? HasBasicEd { get; set; }
    public bool? HasCollege { get; set; }
    public string? CollegeProgramsJson { get; set; }
    public short? SortOrder { get; set; }
}