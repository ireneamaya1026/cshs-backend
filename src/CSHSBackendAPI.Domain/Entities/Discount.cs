using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class Discount : BaseEntity
{
    public long SchoolId { get; set; }
    public string Label { get; set; } = string.Empty;
    public DiscountType Type { get; set; } = DiscountType.Percentage;
    public decimal Value { get; set; }
    public DiscountAppliesTo AppliesTo { get; set; } = DiscountAppliesTo.Tuition;
    public bool IsStackable { get; set; } = false;
    public bool RequiresValidation { get; set; } = false;
    public bool IsActive { get; set; } = true;

    // Relationships
    public School School { get; set; } = null!;
}