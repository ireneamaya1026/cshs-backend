using CSHSBackendAPI.Domain.Common;

namespace CSHSBackendAPI.Domain.Entities;

public class RolePermission : BaseEntity
{
    public long SchoolId { get; set; }
    public string Role { get; set; } = string.Empty;
    public string PagesJson { get; set; } = "[]";
    public string TabsJson { get; set; } = "[]";
    public long? UpdatedBy { get; set; }

    // Relationships
     
}