using ApiToolKit.Application.DTOs.Maps.Roads;
using ApiToolKit.Application.Interfaces.Services.Maps;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Maps;

public class MapsRoadsService : IMapsRoadsService
{
    private readonly IMapsApiClient _apiClient;
    private const string BaseUrl = "https://roads.googleapis.com/v1/snapToRoads";

    public MapsRoadsService(IMapsApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<SnapToRoadsResponse>> SnapToRoadsAsync(SnapToRoadsRequest request)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["path"] = string.Join("|", request.Path),
            ["interpolate"] = request.Interpolate.ToString().ToLower()
        };

        var response = await _apiClient.GetAsync<SnapToRoadsResponse>(BaseUrl, queryParams);
        if (response == null) return Result<SnapToRoadsResponse>.Fail("No response from Google Maps Roads API.");

        return Result<SnapToRoadsResponse>.Ok(response);
    }
}
