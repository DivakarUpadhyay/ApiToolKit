using ApiToolKit.Domain.Entities.Maps;

namespace ApiToolKit.Domain.Interfaces.Repositories.Maps;

public interface IMapsPlaceRepository
{
    Task SavePlaceAsync(PlaceResult place);
    Task<PlaceResult?> GetByPlaceIdAsync(string placeId);
}
