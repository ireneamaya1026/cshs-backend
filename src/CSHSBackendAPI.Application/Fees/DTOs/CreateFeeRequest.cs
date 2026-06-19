namespace CSHSBackendAPI.Application.Fees.DTOs;

public class CreateFeeRequest
{
    public long? CampusId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string GradeLevel { get; set; } = string.Empty;
    public string? StudentType { get; set; }
    public decimal Tuition { get; set; }
    public decimal Misc { get; set; }
    public decimal Lab { get; set; }
    public decimal Books { get; set; }
    public decimal Other { get; set; }
    public decimal EnrollmentFee { get; set; }
}