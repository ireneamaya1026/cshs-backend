namespace CSHSBackendAPI.Application.Announcements.DTOs;

public class UpdateAnnouncementRequest
{
    public string? Title { get; set; }
    public string? Body { get; set; }
    public string? Tag { get; set; }
    public bool? IsPinned { get; set; }
    public string? TargetAudience { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
}