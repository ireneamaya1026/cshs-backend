namespace CSHSBackendAPI.Application.SchoolYears.DTOs;

public class CreateSchoolYearRequest
{
    public string YearLabel { get; set; } = string.Empty;
    public DateOnly DateStart { get; set; }
    public DateOnly DateEnd { get; set; }
}