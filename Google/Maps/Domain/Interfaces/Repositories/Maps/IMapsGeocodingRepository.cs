using ApiToolKit.Domain.Entities.Maps;

namespace ApiToolKit.Domain.Interfaces.Repositories.Maps;

public interface IMapsGeocodingRepository
{
    Task SaveGeocodingResultAsync(GeocodingResult result);
    Task<GeocodingResult?> GetByAddressAsync(string address);
}
