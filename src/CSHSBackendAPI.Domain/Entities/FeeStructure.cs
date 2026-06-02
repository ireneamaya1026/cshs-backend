using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class FeeStructure : BaseEntity
{
    public long SchoolId { get; set; }
    public long? CampusId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string GradeLevel { get; set; } = string.Empty;
    public StudentType? StudentType { get; set; }
    public decimal Tuition { get; set; } = 0;
    public decimal Misc { get; set; } = 0;
    public decimal Lab { get; set; } = 0;
    public decimal Books { get; set; } = 0;
    public decimal Other { get; set; } = 0;
    public decimal EnrollmentFee { get; set; } = 0;
    public decimal TotalFee { get; set; } = 0;
    public bool IsActive { get; set; } = true;

    // Relationships
    public School School { get; set; } = null!;
    public Campus? Campus { get; set; }
}