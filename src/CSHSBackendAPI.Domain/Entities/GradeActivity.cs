using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class GradeActivity : BaseEntity
{
    public long SchoolId { get; set; }
    public long SubjectLoadId { get; set; }
    public GradePeriod Period { get; set; }
    public ActivityCategory Category { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal MaxScore { get; set; } = 100;
    public decimal? Weight { get; set; }
    public short SortOrder { get; set; } = 0;
    public bool IsActive { get; set; } = true;

    // Relationships
    public SubjectLoad SubjectLoad { get; set; } = null!;
    public School School { get; set; } = null!;
    public ICollection<StudentActivityScore> StudentScores { get; set; } = new List<StudentActivityScore>();
}