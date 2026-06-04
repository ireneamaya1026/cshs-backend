namespace CSHSBackendAPI.Application.Config.DTOs;

public class SchoolConfigDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string? Motto { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? WebsiteUrl { get; set; }
    public string PrimaryColor { get; set; } = string.Empty;
    public string SecondaryColor { get; set; } = string.Empty;
    public string? LoginGradientStart { get; set; }
    public string? LoginGradientEnd { get; set; }
    public string? LoginAccentBar { get; set; }
    public string? LoginCardBorder { get; set; }
    public string PortalLabel { get; set; } = string.Empty;
    public string SupportLabel { get; set; } = string.Empty;
    public string PortalWelcome { get; set; } = string.Empty;
    public string? PortalTagline { get; set; }
    public string PortalBgStyle { get; set; } = string.Empty;
    public string? Mission { get; set; }
    public string? Vision { get; set; }
    public string? GoalsJson { get; set; }
    public string? CoreValuesJson { get; set; }
    public string Plan { get; set; } = string.Empty;
    public DateOnly? PlanExpiresAt { get; set; }
}