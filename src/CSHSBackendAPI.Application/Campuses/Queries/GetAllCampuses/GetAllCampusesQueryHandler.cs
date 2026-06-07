using CSHSBackendAPI.Application.Campuses.DTOs;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;

namespace CSHSBackendAPI.Application.Campuses.Queries.GetAllCampuses;

public class GetAllCampusesQueryHandler
{
    private readonly ICampusRepository _campusRepo;

    public GetAllCampusesQueryHandler(ICampusRepository campusRepo) =>
        _campusRepo = campusRepo;

    public async Task<IEnumerable<CampusDto>> Handle()
    {
        var campuses = await _campusRepo.GetAllAsync();

        return campuses.Select(c => new CampusDto
        {
            Id = c.Id,
            CampusKey = c.CampusKey,
            Name = c.Name,
            ShortName = c.ShortName,
            Address = c.Address,
            Phone = c.Phone,
            Email = c.Email,
            HasBasicEd = c.HasBasicEd,
            HasCollege = c.HasCollege,
            CollegeProgramsJson = c.CollegeProgramsJson,
            IsActive = c.IsActive,
            SortOrder = c.SortOrder
        });
    }
}