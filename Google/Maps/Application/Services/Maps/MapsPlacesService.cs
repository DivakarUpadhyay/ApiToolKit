using ApiToolKit.Application.DTOs.Maps.Places;
using ApiToolKit.Application.Interfaces.Services.Maps;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Maps;
using ApiToolKit.Domain.Interfaces.Repositories.Maps;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Maps;

public class MapsPlacesService : IMapsPlacesService
{
    private readonly IMapsApiClient _apiClient;
    private readonly IMapsPlaceRepository _placeRepo;
    private const string NearbyUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json";
    private const string TextSearchUrl = "https://maps.googleapis.com/maps/api/place/textsearch/json";
    private const string DetailsUrl = "https://maps.googleapis.com/maps/api/place/details/json";
    private const string AutocompleteUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json";
    private const string PhotoUrl = "https://maps.googleapis.com/maps/api/place/photo";

    public MapsPlacesService(IMapsApiClient apiClient, IMapsPlaceRepository placeRepo)
    {
        _apiClient = apiClient;
        _placeRepo = placeRepo;
    }

    public async Task<Result<PlacesResponse>> NearbySearchAsync(NearbySearchRequest request)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["location"] = $"{request.Lat},{request.Lng}",
            ["radius"] = request.Radius.ToString(),
            ["language"] = request.Language
        };
        if (!string.IsNullOrEmpty(request.Keyword)) queryParams["keyword"] = request.Keyword;
        if (!string.IsNullOrEmpty(request.Type)) queryParams["type"] = request.Type;
        if (request.MinPrice.HasValue) queryParams["minprice"] = request.MinPrice.Value.ToString();
        if (request.MaxPrice.HasValue) queryParams["maxprice"] = request.MaxPrice.Value.ToString();
        if (request.OpenNow.HasValue) queryParams["opennow"] = request.OpenNow.Value.ToString().ToLower();
        if (!string.IsNullOrEmpty(request.PageToken)) queryParams["pagetoken"] = request.PageToken;

        var response = await _apiClient.GetAsync<PlacesResponse>(NearbyUrl, queryParams);
        if (response == null) return Result<PlacesResponse>.Fail("No response from Google Maps Places API.");
        if (response.Status != "OK" && response.Status != "ZERO_RESULTS")
            return Result<PlacesResponse>.Fail($"Places Nearby Search API error: {response.Status} - {response.ErrorMessage}");

        return Result<PlacesResponse>.Ok(response);
    }

    public async Task<Result<PlacesResponse>> TextSearchAsync(TextSearchRequest request)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["query"] = request.Query,
            ["language"] = request.Language
        };
        if (!string.IsNullOrEmpty(request.Type)) queryParams["type"] = request.Type;
        if (request.Radius.HasValue) queryParams["radius"] = request.Radius.Value.ToString();
        if (request.Lat.HasValue && request.Lng.HasValue)
            queryParams["location"] = $"{request.Lat},{request.Lng}";
        if (!string.IsNullOrEmpty(request.PageToken)) queryParams["pagetoken"] = request.PageToken;
        if (request.MinPrice.HasValue) queryParams["minprice"] = request.MinPrice.Value.ToString();
        if (request.MaxPrice.HasValue) queryParams["maxprice"] = request.MaxPrice.Value.ToString();
        if (request.OpenNow.HasValue) queryParams["opennow"] = request.OpenNow.Value.ToString().ToLower();

        var response = await _apiClient.GetAsync<PlacesResponse>(TextSearchUrl, queryParams);
        if (response == null) return Result<PlacesResponse>.Fail("No response from Google Maps Places API.");
        if (response.Status != "OK" && response.Status != "ZERO_RESULTS")
            return Result<PlacesResponse>.Fail($"Places Text Search API error: {response.Status} - {response.ErrorMessage}");

        return Result<PlacesResponse>.Ok(response);
    }

    public async Task<Result<PlaceResult2>> GetPlaceDetailsAsync(PlaceDetailsRequest request)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["place_id"] = request.PlaceId,
            ["language"] = request.Language,
            ["fields"] = request.Fields
        };
        if (!string.IsNullOrEmpty(request.SessionToken)) queryParams["sessiontoken"] = request.SessionToken;

        var wrapper = await _apiClient.GetAsync<PlaceDetailsWrapper>(DetailsUrl, queryParams);
        if (wrapper == null) return Result<PlaceResult2>.Fail("No response from Google Maps Place Details API.");
        if (wrapper.Status != "OK")
            return Result<PlaceResult2>.Fail($"Place Details API error: {wrapper.Status}");

        var result = wrapper.Result;
        if (result != null)
        {
            var entity = new PlaceResult
            {
                PlaceId = result.PlaceId,
                Name = result.Name,
                FormattedAddress = result.FormattedAddress,
                Lat = result.Geometry?.Location?.Lat ?? 0,
                Lng = result.Geometry?.Location?.Lng ?? 0,
                Rating = result.Rating ?? 0,
                UserRatingsTotal = result.UserRatingsTotal ?? 0,
                Types = JsonConvert.SerializeObject(result.Types),
                PhoneNumber = result.FormattedPhoneNumber,
                Website = result.Website,
                BusinessStatus = result.BusinessStatus,
                OpenNow = result.OpeningHours?.OpenNow,
                PriceLevel = result.PriceLevel,
                CreatedAt = DateTime.UtcNow
            };
            await _placeRepo.SavePlaceAsync(entity);
        }

        return Result<PlaceResult2>.Ok(result ?? new PlaceResult2());
    }

    public async Task<Result<PlaceAutocompleteResponse>> AutoCompleteAsync(PlaceAutoCompleteRequest request)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["input"] = request.Input,
            ["language"] = request.Language,
            ["strictbounds"] = request.StrictBounds.ToString().ToLower()
        };
        if (!string.IsNullOrEmpty(request.Types)) queryParams["types"] = request.Types;
        if (!string.IsNullOrEmpty(request.Components)) queryParams["components"] = request.Components;
        if (request.Lat.HasValue && request.Lng.HasValue)
        {
            queryParams["location"] = $"{request.Lat},{request.Lng}";
            if (request.Radius.HasValue) queryParams["radius"] = request.Radius.Value.ToString();
        }
        if (!string.IsNullOrEmpty(request.SessionToken)) queryParams["sessiontoken"] = request.SessionToken;

        var response = await _apiClient.GetAsync<PlaceAutocompleteResponse>(AutocompleteUrl, queryParams);
        if (response == null) return Result<PlaceAutocompleteResponse>.Fail("No response from Google Maps Autocomplete API.");
        if (response.Status != "OK" && response.Status != "ZERO_RESULTS")
            return Result<PlaceAutocompleteResponse>.Fail($"Autocomplete API error: {response.Status} - {response.ErrorMessage}");

        return Result<PlaceAutocompleteResponse>.Ok(response);
    }

    public async Task<Result<string>> GetPlacePhotoAsync(string photoReference, int maxWidth)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["photoreference"] = photoReference,
            ["maxwidth"] = maxWidth.ToString()
        };

        // Build URL and return it — the actual image is served directly by the API
        var sb = new System.Text.StringBuilder(PhotoUrl + "?");
        foreach (var kv in queryParams)
            sb.Append($"{kv.Key}={Uri.EscapeDataString(kv.Value)}&");

        return Result<string>.Ok(sb.ToString().TrimEnd('&'));
    }

    private class PlaceDetailsWrapper
    {
        public string Status { get; set; } = string.Empty;
        public PlaceResult2? Result { get; set; }
    }
}
