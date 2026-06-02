using CSHSBackendAPI.Domain.Common;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Domain.Entities;

public class School : BaseEntity
{
    public Guid Uuid { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string? Motto { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? WebsiteUrl { get; set; }
    // Branding
    public string PrimaryColor { get; set; } = "#750014";
    public string SecondaryColor { get; set; } = "#080c42";
    public string? LoginGradientStart { get; set; }
    public string? LoginGradientEnd { get; set; }
    public string? LoginAccentBar { get; set; }
    public string? LoginCardBorder { get; set; }
    // Portal labels
    public string PortalLabel { get; set; } = "School Management Portal";
    public string SupportLabel { get; set; } = "Contact IT Support";
    public string PortalWelcome { get; set; } = "Welcome back.";
    public string? PortalTagline { get; set; }
    public PortalBgStyle PortalBgStyle { get; set; } = PortalBgStyle.Gradient;
    // Content
    public string? Mission { get; set; }
    public string? Vision { get; set; }
    public string? GoalsJson { get; set; }
    public string? CoreValuesJson { get; set; }
    // Status
    public bool IsActive { get; set; } = true;
    public SchoolPlan Plan { get; set; } = SchoolPlan.Basic;

    public string Slug { get; set; } = string.Empty; 

    // Relationships
    public ICollection<Campus> Campuses { get; set; } = new List<Campus>();
    public ICollection<SchoolYear> SchoolYears { get; set; } = new List<SchoolYear>();
    public ICollection<SystemUser> SystemUsers { get; set; } = new List<SystemUser>();
}