namespace CSHSBackendAPI.Application.Announcements.DTOs;

public class CreateAnnouncementRequest
{
    public long? CampusId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Body { get; set; }
    public string? Tag { get; set; }
    public bool IsPinned { get; set; } = false;
    public string TargetAudience { get; set; } = "all";
    public DateTime? PublishedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
}