using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class Enrollment : BaseEntity
{
    public long SchoolId { get; set; }
    public long CampusId { get; set; }
    public string ReferenceNo { get; set; } = string.Empty;
    public long? StudentId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public Department Department { get; set; } = Department.BasicEd;
    public EnrollmentSource Source { get; set; } = EnrollmentSource.Admin;
    // Student info snapshot
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? Suffix { get; set; }
    public DateOnly? Birthdate { get; set; }
    public Sex? Sex { get; set; }
    public string? ContactNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    // Academic
    public string GradeLevel { get; set; } = string.Empty;
    public string? Section { get; set; }
    public string? Program { get; set; }
    public StudentType StudentType { get; set; } = StudentType.New;
    public string? Lrn { get; set; }
    // Workflow
    public string WorkflowId { get; set; } = string.Empty;
    public short WorkflowVersion { get; set; } = 1;
    public string CurrentStep { get; set; } = string.Empty;
    public string? PreviousStep { get; set; }
    // Fees
    public decimal AssessedFees { get; set; } = 0;
    public string? DiscountJson { get; set; }
    public decimal NetFee { get; set; } = 0;
    public decimal AmountPaid { get; set; } = 0;
    public decimal Balance { get; set; } = 0;
    // Flags
    public bool HasMissingDocs { get; set; } = false;
    public bool ConvertedToStudent { get; set; } = false;
    public string? SourceReferenceNo { get; set; }
    public DateTime? SubmittedAt { get; set; }

    // Relationships
    public School School { get; set; } = null!;
    public Campus Campus { get; set; } = null!;
    public Student? Student { get; set; }
    public ICollection<EnrollmentStageHistory> StageHistories { get; set; } = new List<EnrollmentStageHistory>();
    public ICollection<EnrollmentPayment> Payments { get; set; } = new List<EnrollmentPayment>();
}