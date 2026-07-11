using ApiToolKit.Application.DTOs.Maps.Geocoding;
using ApiToolKit.Application.Interfaces.Services.Maps;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Maps;
using ApiToolKit.Domain.Interfaces.Repositories.Maps;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Maps;

public class MapsGeocodingService : IMapsGeocodingService
{
    private readonly IMapsApiClient _apiClient;
    private readonly IMapsGeocodingRepository _geocodingRepo;
    private const string BaseUrl = "https://maps.googleapis.com/maps/api/geocode/json";

    public MapsGeocodingService(IMapsApiClient apiClient, IMapsGeocodingRepository geocodingRepo)
    {
        _apiClient = apiClient;
        _geocodingRepo = geocodingRepo;
    }

    public async Task<Result<GeocodeResponse>> GeocodeAsync(GeocodeAddressRequest request)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["address"] = request.Address,
            ["language"] = request.Language
        };
        if (!string.IsNullOrEmpty(request.Region)) queryParams["region"] = request.Region;
        if (!string.IsNullOrEmpty(request.Components)) queryParams["components"] = request.Components;
        if (!string.IsNullOrEmpty(request.Bounds)) queryParams["bounds"] = request.Bounds;

        var response = await _apiClient.GetAsync<GeocodeResponse>(BaseUrl, queryParams);
        if (response == null) return Result<GeocodeResponse>.Fail("No response from Google Maps Geocoding API.");
        if (response.Status != "OK" && response.Status != "ZERO_RESULTS")
            return Result<GeocodeResponse>.Fail($"Geocoding API error: {response.Status} - {response.ErrorMessage}");

        if (response.Results.Count > 0)
        {
            var first = response.Results[0];
            var entity = new GeocodingResult
            {
                PlaceId = first.PlaceId,
                FormattedAddress = first.FormattedAddress,
                Lat = first.Geometry.Location.Lat,
                Lng = first.Geometry.Location.Lng,
                LocationType = first.Geometry.LocationType,
                AddressComponents = JsonConvert.SerializeObject(first.AddressComponents),
                CreatedAt = DateTime.UtcNow
            };
            await _geocodingRepo.SaveGeocodingResultAsync(entity);
        }

        return Result<GeocodeResponse>.Ok(response);
    }

    public async Task<Result<GeocodeResponse>> ReverseGeocodeAsync(ReverseGeocodeRequest request)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["latlng"] = $"{request.Lat},{request.Lng}",
            ["language"] = request.Language
        };
        if (!string.IsNullOrEmpty(request.ResultType)) queryParams["result_type"] = request.ResultType;
        if (!string.IsNullOrEmpty(request.LocationType)) queryParams["location_type"] = request.LocationType;

        var response = await _apiClient.GetAsync<GeocodeResponse>(BaseUrl, queryParams);
        if (response == null) return Result<GeocodeResponse>.Fail("No response from Google Maps Geocoding API.");
        if (response.Status != "OK" && response.Status != "ZERO_RESULTS")
            return Result<GeocodeResponse>.Fail($"Reverse Geocoding API error: {response.Status} - {response.ErrorMessage}");

        return Result<GeocodeResponse>.Ok(response);
    }
}
