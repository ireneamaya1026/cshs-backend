using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Config.DTOs;
using CSHSBackendAPI.Domain.Enums;

namespace CSHSBackendAPI.Application.Config.Commands.UpdateConfig;

public class UpdateConfigCommandHandler
{
    private readonly ISchoolConfigRepository _configRepo;

    public UpdateConfigCommandHandler(ISchoolConfigRepository configRepo) =>
        _configRepo = configRepo;

    public async Task<SchoolConfigDto> Handle(UpdateConfigRequest request)
    {
        var config = await _configRepo.GetAsync()
            ?? throw new NotFoundException("SchoolConfig", 1);

        // Only update fields that were provided (PATCH behavior)
        if (request.Name != null) config.Name = request.Name;
        if (request.ShortName != null) config.ShortName = request.ShortName;
        if (request.LogoUrl != null) config.LogoUrl = request.LogoUrl;
        if (request.Motto != null) config.Motto = request.Motto;
        if (request.Address != null) config.Address = request.Address;
        if (request.Email != null) config.Email = request.Email;
        if (request.Phone != null) config.Phone = request.Phone;
        if (request.WebsiteUrl != null) config.WebsiteUrl = request.WebsiteUrl;
        if (request.PrimaryColor != null) config.PrimaryColor = request.PrimaryColor;
        if (request.SecondaryColor != null) config.SecondaryColor = request.SecondaryColor;
        if (request.LoginGradientStart != null) config.LoginGradientStart = request.LoginGradientStart;
        if (request.LoginGradientEnd != null) config.LoginGradientEnd = request.LoginGradientEnd;
        if (request.LoginAccentBar != null) config.LoginAccentBar = request.LoginAccentBar;
        if (request.LoginCardBorder != null) config.LoginCardBorder = request.LoginCardBorder;
        if (request.PortalLabel != null) config.PortalLabel = request.PortalLabel;
        if (request.SupportLabel != null) config.SupportLabel = request.SupportLabel;
        if (request.PortalWelcome != null) config.PortalWelcome = request.PortalWelcome;
        if (request.PortalTagline != null) config.PortalTagline = request.PortalTagline;
        if (request.Mission != null) config.Mission = request.Mission;
        if (request.Vision != null) config.Vision = request.Vision;
        if (request.GoalsJson != null) config.GoalsJson = request.GoalsJson;
        if (request.CoreValuesJson != null) config.CoreValuesJson = request.CoreValuesJson;

        if (request.PortalBgStyle != null &&
            Enum.TryParse<PortalBgStyle>(request.PortalBgStyle, true, out var bgStyle))
            config.PortalBgStyle = bgStyle;

        await _configRepo.UpdateAsync(config);

        return new SchoolConfigDto
        {
            Id = config.Id,
            Name = config.Name,
            ShortName = config.ShortName,
            LogoUrl = config.LogoUrl,
            Motto = config.Motto,
            Address = config.Address,
            Email = config.Email,
            Phone = config.Phone,
            WebsiteUrl = config.WebsiteUrl,
            PrimaryColor = config.PrimaryColor,
            SecondaryColor = config.SecondaryColor,
            LoginGradientStart = config.LoginGradientStart,
            LoginGradientEnd = config.LoginGradientEnd,
            LoginAccentBar = config.LoginAccentBar,
            LoginCardBorder = config.LoginCardBorder,
            PortalLabel = config.PortalLabel,
            SupportLabel = config.SupportLabel,
            PortalWelcome = config.PortalWelcome,
            PortalTagline = config.PortalTagline,
            PortalBgStyle = config.PortalBgStyle.ToString(),
            Mission = config.Mission,
            Vision = config.Vision,
            GoalsJson = config.GoalsJson,
            CoreValuesJson = config.CoreValuesJson,
            Plan = config.Plan.ToString(),
            PlanExpiresAt = config.PlanExpiresAt
        };
    }
}