using ApiToolKit.Domain.Entities.Maps;
using ApiToolKit.Domain.Interfaces.Repositories.Maps;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Maps;

public class MapsPlaceRepository : IMapsPlaceRepository
{
    private readonly DapperContext _context;

    public MapsPlaceRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SavePlaceAsync(PlaceResult place)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Maps_SavePlaceResult",
            new
            {
                place.PlaceId,
                place.Name,
                place.FormattedAddress,
                place.Lat,
                place.Lng,
                place.Rating,
                place.UserRatingsTotal,
                place.Types,
                place.PhoneNumber,
                place.Website,
                place.BusinessStatus,
                place.OpenNow,
                place.PriceLevel
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<PlaceResult?> GetByPlaceIdAsync(string placeId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<PlaceResult>(
            "sp_Maps_GetPlaceByPlaceId",
            new { PlaceId = placeId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
