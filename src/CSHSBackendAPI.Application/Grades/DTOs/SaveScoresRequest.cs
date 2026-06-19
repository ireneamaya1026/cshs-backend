namespace CSHSBackendAPI.Application.Grades.DTOs;

public class SaveScoresRequest
{
    public long ActivityId { get; set; }
    public List<StudentScoreItem> Scores { get; set; } = new();
}

public class StudentScoreItem
{
    public long StudentId { get; set; }
    public decimal? Score { get; set; }
    public string? Remarks { get; set; }
}