using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class SchoolConfig : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string? Motto { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? WebsiteUrl { get; set; }
    public string PrimaryColor { get; set; } = "#750014";
    public string SecondaryColor { get; set; } = "#080c42";
    public string? LoginGradientStart { get; set; }
    public string? LoginGradientEnd { get; set; }
    public string? LoginAccentBar { get; set; }
    public string? LoginCardBorder { get; set; }
    public string PortalLabel { get; set; } = "School Management Portal";
    public string SupportLabel { get; set; } = "Contact IT Support";
    public string PortalWelcome { get; set; } = "Welcome back.";
    public string? PortalTagline { get; set; }
    public PortalBgStyle PortalBgStyle { get; set; } = PortalBgStyle.Gradient;
    public string? Mission { get; set; }
    public string? Vision { get; set; }
    public string? GoalsJson { get; set; }
    public string? CoreValuesJson { get; set; }
    public SchoolPlan Plan { get; set; } = SchoolPlan.Basic;
    public DateOnly? PlanExpiresAt { get; set; }
}