namespace CSHSBackendAPI.Application.Clearances.DTOs;

public class ClearanceDto
{
    public long Id { get; set; }
    public long CampusId { get; set; }
    public long StudentId { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string? Reason { get; set; }
    public bool HasUnpaidBalance { get; set; }
    public bool IsFullyCleared { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public List<DeptSignoffDto> DeptSignoffs { get; set; } = new();
}

public class DeptSignoffDto
{
    public long Id { get; set; }
    public string DeptId { get; set; } = string.Empty;
    public string DeptLabel { get; set; } = string.Empty;
    public bool IsCleared { get; set; }
    public string? ClearedByName { get; set; }
    public DateTime? ClearedAt { get; set; }
    public bool IsAutoCleared { get; set; }
}