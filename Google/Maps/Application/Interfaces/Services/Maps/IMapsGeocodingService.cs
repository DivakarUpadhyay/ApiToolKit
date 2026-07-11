using ApiToolKit.Application.DTOs.Maps.Geocoding;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Maps;

public interface IMapsGeocodingService
{
    Task<Result<GeocodeResponse>> GeocodeAsync(GeocodeAddressRequest request);
    Task<Result<GeocodeResponse>> ReverseGeocodeAsync(ReverseGeocodeRequest request);
}
