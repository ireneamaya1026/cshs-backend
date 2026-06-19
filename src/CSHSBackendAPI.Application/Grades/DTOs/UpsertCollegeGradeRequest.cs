namespace CSHSBackendAPI.Application.Grades.DTOs;

public class UpsertCollegeGradeRequest
{
    public long StudentId { get; set; }
    public long CampusId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string Semester { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string? Program { get; set; }
    public string? YearLevel { get; set; }
    public long? SubjectLoadId { get; set; }
    public decimal? Prelim { get; set; }
    public decimal? Midterm { get; set; }
    public decimal? Finals { get; set; }
    public decimal? SemesterGrade { get; set; }
    public decimal? PointGrade { get; set; }
    public string? Descriptor { get; set; }
    public string? SpecialGrade { get; set; }
}