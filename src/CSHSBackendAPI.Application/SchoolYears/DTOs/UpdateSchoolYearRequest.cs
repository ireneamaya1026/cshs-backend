namespace CSHSBackendAPI.Application.SchoolYears.DTOs;

public class UpdateSchoolYearRequest
{
    public bool? IsCurrent { get; set; }
    public bool? IsLocked { get; set; }
}