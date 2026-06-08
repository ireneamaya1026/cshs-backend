namespace CSHSBackendAPI.Application.Enrollments.DTOs;

public class EnrollmentStageHistoryDto
{
    public long Id { get; set; }
    public string Step { get; set; } = string.Empty;
    public string? FromStep { get; set; }
    public string? ActionId { get; set; }
    public string ByName { get; set; } = string.Empty;
    public string ByRole { get; set; } = string.Empty;
    public string? Note { get; set; }
    public DateTime RecordedAt { get; set; }
}