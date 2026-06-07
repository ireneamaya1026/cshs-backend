using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Config.DTOs;

namespace CSHSBackendAPI.Application.Config.Queries.GetConfig;

public class GetConfigQueryHandler
{
    private readonly ISchoolConfigRepository _configRepo;

    public GetConfigQueryHandler(ISchoolConfigRepository configRepo) =>
        _configRepo = configRepo;

    public async Task<SchoolConfigDto> Handle()
    {
        var config = await _configRepo.GetAsync()
            ?? throw new NotFoundException("SchoolConfig", 1);

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