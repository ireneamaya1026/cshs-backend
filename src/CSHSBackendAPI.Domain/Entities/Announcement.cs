using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class Announcement : BaseEntity
{
    public long SchoolId { get; set; }
    public long? CampusId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Body { get; set; }
    public string? Tag { get; set; }
    public bool IsPinned { get; set; } = false;
    public AnnouncementAudience TargetAudience { get; set; } = AnnouncementAudience.All;
    public DateTime? PublishedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public long? CreatedById { get; set; }

    // Relationships
     
    public Campus? Campus { get; set; }
    public SystemUser? CreatedBy { get; set; }
}