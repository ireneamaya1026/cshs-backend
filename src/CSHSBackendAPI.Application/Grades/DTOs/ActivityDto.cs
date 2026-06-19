namespace CSHSBackendAPI.Application.Grades.DTOs;

public class ActivityDto
{
    public long Id { get; set; }
    public long SubjectLoadId { get; set; }
    public string Period { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public decimal MaxScore { get; set; }
    public decimal? Weight { get; set; }
    public short SortOrder { get; set; }
    public bool IsActive { get; set; }
}