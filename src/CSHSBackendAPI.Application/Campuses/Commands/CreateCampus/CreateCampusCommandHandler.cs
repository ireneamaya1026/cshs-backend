using CSHSBackendAPI.Application.Campuses.DTOs;
using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Campuses.Commands.CreateCampus;

public class CreateCampusCommandHandler
{
    private readonly ICampusRepository _campusRepo;

    public CreateCampusCommandHandler(ICampusRepository campusRepo) =>
        _campusRepo = campusRepo;

    public async Task<CampusDto> Handle(CreateCampusRequest request)
    {
        // Check campus key is unique
        if (await _campusRepo.CampusKeyExistsAsync(request.CampusKey))
            throw new ValidationException(new List<string>
            {
                $"Campus key '{request.CampusKey}' already exists."
            });

        var campus = new Campus
        {
            CampusKey = request.CampusKey.ToUpper(),
            Name = request.Name,
            ShortName = request.ShortName,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email,
            HasBasicEd = request.HasBasicEd,
            HasCollege = request.HasCollege,
            CollegeProgramsJson = request.CollegeProgramsJson,
            SortOrder = request.SortOrder,
            IsActive = true
        };

        var created = await _campusRepo.CreateAsync(campus);

        return new CampusDto
        {
            Id = created.Id,
            CampusKey = created.CampusKey,
            Name = created.Name,
            ShortName = created.ShortName,
            Address = created.Address,
            Phone = created.Phone,
            Email = created.Email,
            HasBasicEd = created.HasBasicEd,
            HasCollege = created.HasCollege,
            CollegeProgramsJson = created.CollegeProgramsJson,
            IsActive = created.IsActive,
            SortOrder = created.SortOrder
        };
    }
}