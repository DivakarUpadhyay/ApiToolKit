using ApiToolKit.Application.DTOs.Maps.TimeZone;
using ApiToolKit.Application.Interfaces.Services.Maps;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Maps;

public class MapsTimeZoneService : IMapsTimeZoneService
{
    private readonly IMapsApiClient _apiClient;
    private const string BaseUrl = "https://maps.googleapis.com/maps/api/timezone/json";

    public MapsTimeZoneService(IMapsApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<TimeZoneResponse>> GetTimeZoneAsync(TimeZoneRequest request)
    {
        var timestamp = request.Timestamp > 0
            ? request.Timestamp
            : DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var queryParams = new Dictionary<string, string>
        {
            ["location"] = $"{request.Lat},{request.Lng}",
            ["timestamp"] = timestamp.ToString()
        };

        var response = await _apiClient.GetAsync<TimeZoneResponse>(BaseUrl, queryParams);
        if (response == null) return Result<TimeZoneResponse>.Fail("No response from Google Maps Time Zone API.");
        if (response.Status != "OK")
            return Result<TimeZoneResponse>.Fail($"Time Zone API error: {response.Status} - {response.ErrorMessage}");

        return Result<TimeZoneResponse>.Ok(response);
    }
}
