using CSHSBackendAPI.Domain.Common;

namespace CSHSBackendAPI.Domain.Entities;

public class Campus : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int SchoolId { get; set; }

    // Relationships
    public School School { get; set; } = null!;
    public ICollection<User> Users { get; set; } = new List<User>();
}