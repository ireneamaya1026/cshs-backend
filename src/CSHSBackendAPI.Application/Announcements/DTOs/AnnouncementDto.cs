namespace CSHSBackendAPI.Application.Announcements.DTOs;

public class AnnouncementDto
{
    public long Id { get; set; }
    public long? CampusId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Body { get; set; }
    public string? Tag { get; set; }
    public bool IsPinned { get; set; }
    public string TargetAudience { get; set; } = string.Empty;
    public DateTime? PublishedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
}