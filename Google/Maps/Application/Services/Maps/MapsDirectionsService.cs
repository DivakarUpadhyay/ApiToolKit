using ApiToolKit.Application.DTOs.Maps.Directions;
using ApiToolKit.Application.Interfaces.Services.Maps;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Maps;
using ApiToolKit.Domain.Interfaces.Repositories.Maps;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Maps;

public class MapsDirectionsService : IMapsDirectionsService
{
    private readonly IMapsApiClient _apiClient;
    private readonly IMapsGeocodingRepository _geocodingRepo;
    private const string BaseUrl = "https://maps.googleapis.com/maps/api/directions/json";

    public MapsDirectionsService(IMapsApiClient apiClient, IMapsGeocodingRepository geocodingRepo)
    {
        _apiClient = apiClient;
        _geocodingRepo = geocodingRepo;
    }

    public async Task<Result<DirectionsResponse>> GetDirectionsAsync(DirectionsRequest request)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["origin"] = request.Origin,
            ["destination"] = request.Destination,
            ["mode"] = request.Mode,
            ["language"] = request.Language,
            ["units"] = request.Units,
            ["alternatives"] = request.Alternatives.ToString().ToLower()
        };
        if (!string.IsNullOrEmpty(request.Avoid)) queryParams["avoid"] = request.Avoid;
        if (!string.IsNullOrEmpty(request.DepartureTime)) queryParams["departure_time"] = request.DepartureTime;
        if (!string.IsNullOrEmpty(request.ArrivalTime)) queryParams["arrival_time"] = request.ArrivalTime;
        if (request.WaypointsList.Count > 0)
            queryParams["waypoints"] = string.Join("|", request.WaypointsList);

        var response = await _apiClient.GetAsync<DirectionsResponse>(BaseUrl, queryParams);
        if (response == null) return Result<DirectionsResponse>.Fail("No response from Google Maps Directions API.");
        if (response.Status != "OK" && response.Status != "ZERO_RESULTS")
            return Result<DirectionsResponse>.Fail($"Directions API error: {response.Status} - {response.ErrorMessage}");

        if (response.Routes.Count > 0 && response.Routes[0].Legs.Count > 0)
        {
            var leg = response.Routes[0].Legs[0];
            var entity = new DirectionsResult
            {
                Origin = request.Origin,
                Destination = request.Destination,
                Mode = request.Mode,
                Distance = leg.Distance.Value,
                Duration = leg.Duration.Value,
                DistanceText = leg.Distance.Text,
                DurationText = leg.Duration.Text,
                Steps = JsonConvert.SerializeObject(leg.Steps),
                EncodedPolyline = response.Routes[0].OverviewPolyline.Points,
                CreatedAt = DateTime.UtcNow
            };
            // Log stored via repository if needed
            _ = entity;
        }

        return Result<DirectionsResponse>.Ok(response);
    }
}
