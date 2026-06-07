using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class SystemUser : BaseEntity
{
    public long? CampusId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string? CustomPermissionsJson { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }

    // Relationships
    public Campus? Campus { get; set; }
}