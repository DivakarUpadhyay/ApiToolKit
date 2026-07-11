using ApiToolKit.Application.DTOs.Maps.DistanceMatrix;
using ApiToolKit.Application.Interfaces.Services.Maps;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Maps;

public class MapsDistanceMatrixService : IMapsDistanceMatrixService
{
    private readonly IMapsApiClient _apiClient;
    private const string BaseUrl = "https://maps.googleapis.com/maps/api/distancematrix/json";

    public MapsDistanceMatrixService(IMapsApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<DistanceMatrixResponse>> GetDistanceMatrixAsync(DistanceMatrixRequest request)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["origins"] = string.Join("|", request.Origins),
            ["destinations"] = string.Join("|", request.Destinations),
            ["mode"] = request.Mode,
            ["language"] = request.Language,
            ["units"] = request.Units
        };
        if (!string.IsNullOrEmpty(request.Avoid)) queryParams["avoid"] = request.Avoid;
        if (!string.IsNullOrEmpty(request.DepartureTime)) queryParams["departure_time"] = request.DepartureTime;
        if (!string.IsNullOrEmpty(request.TrafficModel)) queryParams["traffic_model"] = request.TrafficModel;

        var response = await _apiClient.GetAsync<DistanceMatrixResponse>(BaseUrl, queryParams);
        if (response == null) return Result<DistanceMatrixResponse>.Fail("No response from Google Maps Distance Matrix API.");
        if (response.Status != "OK" && response.Status != "ZERO_RESULTS")
            return Result<DistanceMatrixResponse>.Fail($"Distance Matrix API error: {response.Status} - {response.ErrorMessage}");

        return Result<DistanceMatrixResponse>.Ok(response);
    }
}
