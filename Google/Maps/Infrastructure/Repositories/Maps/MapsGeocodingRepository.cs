using ApiToolKit.Domain.Entities.Maps;
using ApiToolKit.Domain.Interfaces.Repositories.Maps;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Maps;

public class MapsGeocodingRepository : IMapsGeocodingRepository
{
    private readonly DapperContext _context;

    public MapsGeocodingRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveGeocodingResultAsync(GeocodingResult result)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Maps_SaveGeocodingResult",
            new
            {
                result.PlaceId,
                result.FormattedAddress,
                result.Lat,
                result.Lng,
                result.LocationType,
                result.AddressComponents
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<GeocodingResult?> GetByAddressAsync(string address)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<GeocodingResult>(
            "sp_Maps_GetGeocodingResultByAddress",
            new { Address = address },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
