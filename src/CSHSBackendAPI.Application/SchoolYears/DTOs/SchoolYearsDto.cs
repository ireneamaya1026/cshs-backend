namespace CSHSBackendAPI.Application.SchoolYears.DTOs;

public class SchoolYearDto
{
    public long Id { get; set; }
    public string YearLabel { get; set; } = string.Empty;
    public DateOnly DateStart { get; set; }
    public DateOnly DateEnd { get; set; }
    public bool IsCurrent { get; set; }
    public bool IsLocked { get; set; }
}