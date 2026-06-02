using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class PushToken : BaseEntity
{
    public long SchoolId { get; set; }
    public long? StudentId { get; set; }
    public string Token { get; set; } = string.Empty;
    public PushTokenPlatform Platform { get; set; } = PushTokenPlatform.Web;
    public bool IsActive { get; set; } = true;
    public DateTime? LastUsedAt { get; set; }

    // Relationships
    public School School { get; set; } = null!;
    public Student? Student { get; set; }
}