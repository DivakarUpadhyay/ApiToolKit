using ApiToolKit.Application.DTOs.Maps.Elevation;
using ApiToolKit.Application.Interfaces.Services.Maps;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Maps;

public class MapsElevationService : IMapsElevationService
{
    private readonly IMapsApiClient _apiClient;
    private const string BaseUrl = "https://maps.googleapis.com/maps/api/elevation/json";

    public MapsElevationService(IMapsApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<ElevationResponse>> GetElevationAsync(ElevationRequest request)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["locations"] = string.Join("|", request.Locations)
        };

        var response = await _apiClient.GetAsync<ElevationResponse>(BaseUrl, queryParams);
        if (response == null) return Result<ElevationResponse>.Fail("No response from Google Maps Elevation API.");
        if (response.Status != "OK")
            return Result<ElevationResponse>.Fail($"Elevation API error: {response.Status} - {response.ErrorMessage}");

        return Result<ElevationResponse>.Ok(response);
    }

    public async Task<Result<ElevationResponse>> GetElevationAlongPathAsync(ElevationPathRequest request)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["path"] = string.Join("|", request.Path),
            ["samples"] = request.Samples.ToString()
        };

        var response = await _apiClient.GetAsync<ElevationResponse>(BaseUrl, queryParams);
        if (response == null) return Result<ElevationResponse>.Fail("No response from Google Maps Elevation API.");
        if (response.Status != "OK")
            return Result<ElevationResponse>.Fail($"Elevation Along Path API error: {response.Status} - {response.ErrorMessage}");

        return Result<ElevationResponse>.Ok(response);
    }
}
