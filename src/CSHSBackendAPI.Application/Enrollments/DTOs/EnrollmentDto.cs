namespace CSHSBackendAPI.Application.Enrollments.DTOs;

public class EnrollmentDto
{
    public long Id { get; set; }
    public string ReferenceNo { get; set; } = string.Empty;
    public long CampusId { get; set; }
    public string CampusName { get; set; } = string.Empty;
    public long? StudentId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    // Student snapshot
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? Suffix { get; set; }
    public DateOnly? Birthdate { get; set; }
    public string? Sex { get; set; }
    public string? ContactNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    // Academic
    public string GradeLevel { get; set; } = string.Empty;
    public string? Section { get; set; }
    public string? Program { get; set; }
    public string StudentType { get; set; } = string.Empty;
    public string? Lrn { get; set; }
    // Workflow
    public string WorkflowId { get; set; } = string.Empty;
    public string CurrentStep { get; set; } = string.Empty;
    public string? PreviousStep { get; set; }
    // Fees
    public decimal AssessedFees { get; set; }
    public decimal NetFee { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal Balance { get; set; }
    // Flags
    public bool HasMissingDocs { get; set; }
    public bool ConvertedToStudent { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    // Stage history
    public List<EnrollmentStageHistoryDto>? StageHistories { get; set; }
}