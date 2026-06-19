namespace CSHSBackendAPI.Application.Grades.DTOs;

public class CreateActivityRequest
{
    public long SubjectLoadId { get; set; }
    public string Period { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public decimal MaxScore { get; set; } = 100;
    public decimal? Weight { get; set; }
    public short SortOrder { get; set; } = 0;
}