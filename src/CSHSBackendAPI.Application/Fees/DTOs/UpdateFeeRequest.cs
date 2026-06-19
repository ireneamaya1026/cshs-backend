namespace CSHSBackendAPI.Application.Fees.DTOs;

public class UpdateFeeRequest
{
    public decimal? Tuition { get; set; }
    public decimal? Misc { get; set; }
    public decimal? Lab { get; set; }
    public decimal? Books { get; set; }
    public decimal? Other { get; set; }
    public decimal? EnrollmentFee { get; set; }
}