using ApiToolKit.Application.DTOs.Maps.Places;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Maps;

public interface IMapsPlacesService
{
    Task<Result<PlacesResponse>> NearbySearchAsync(NearbySearchRequest request);
    Task<Result<PlacesResponse>> TextSearchAsync(TextSearchRequest request);
    Task<Result<PlaceResult2>> GetPlaceDetailsAsync(PlaceDetailsRequest request);
    Task<Result<PlaceAutocompleteResponse>> AutoCompleteAsync(PlaceAutoCompleteRequest request);
    Task<Result<string>> GetPlacePhotoAsync(string photoReference, int maxWidth);
}
