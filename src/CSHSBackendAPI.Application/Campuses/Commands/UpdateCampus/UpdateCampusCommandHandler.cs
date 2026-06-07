using CSHSBackendAPI.Application.Campuses.DTOs;
using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;

namespace CSHSBackendAPI.Application.Campuses.Commands.UpdateCampus;

public class UpdateCampusCommandHandler
{
    private readonly ICampusRepository _campusRepo;

    public UpdateCampusCommandHandler(ICampusRepository campusRepo) =>
        _campusRepo = campusRepo;

    public async Task<CampusDto> Handle(long id, UpdateCampusRequest request)
    {
        var campus = await _campusRepo.GetByIdAsync(id)
            ?? throw new NotFoundException("Campus", id);

        if (request.CampusKey != null) campus.CampusKey = request.CampusKey.ToUpper();
        if (request.Name != null) campus.Name = request.Name;
        if (request.ShortName != null) campus.ShortName = request.ShortName;
        if (request.Address != null) campus.Address = request.Address;
        if (request.Phone != null) campus.Phone = request.Phone;
        if (request.Email != null) campus.Email = request.Email;
        if (request.HasBasicEd.HasValue) campus.HasBasicEd = request.HasBasicEd.Value;
        if (request.HasCollege.HasValue) campus.HasCollege = request.HasCollege.Value;
        if (request.CollegeProgramsJson != null) campus.CollegeProgramsJson = request.CollegeProgramsJson;
        if (request.SortOrder.HasValue) campus.SortOrder = request.SortOrder.Value;

        await _campusRepo.UpdateAsync(campus);

        return new CampusDto
        {
            Id = campus.Id,
            CampusKey = campus.CampusKey,
            Name = campus.Name,
            ShortName = campus.ShortName,
            Address = campus.Address,
            Phone = campus.Phone,
            Email = campus.Email,
            HasBasicEd = campus.HasBasicEd,
            HasCollege = campus.HasCollege,
            CollegeProgramsJson = campus.CollegeProgramsJson,
            IsActive = campus.IsActive,
            SortOrder = campus.SortOrder
        };
    }
}