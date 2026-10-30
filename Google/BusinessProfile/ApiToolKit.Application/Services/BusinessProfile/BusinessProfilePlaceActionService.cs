using ApiToolKit.Application.DTOs.BusinessProfile.PlaceAction;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.BusinessProfile;

namespace ApiToolKit.Application.Services.BusinessProfile;

public class BusinessProfilePlaceActionService : BusinessProfileServiceBase, IBusinessProfilePlaceActionService
{
    public BusinessProfilePlaceActionService(IBusinessProfileApiClient apiClient, IBusinessProfileAuthService authService, IBusinessProfileAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListPlaceActionLinksAsync(ListPlaceActionLinksRequest r)
    {
        var url = $"https://mybusinessplaceactions.googleapis.com/v1/locations/{r.LocationId}/placeActionLinks";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "filter", r.Filter);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListPlaceActionLinks", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetPlaceActionLinkAsync(GetPlaceActionLinkRequest r)
    {
        var url = $"https://mybusinessplaceactions.googleapis.com/v1/locations/{r.LocationId}/placeActionLinks/{r.LinkId}";
        return await SendAsync(r.AccountKey, "GetPlaceActionLink", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreatePlaceActionLinkAsync(CreatePlaceActionLinkRequest r)
    {
        var url = $"https://mybusinessplaceactions.googleapis.com/v1/locations/{r.LocationId}/placeActionLinks";
        return await SendAsync(r.AccountKey, "CreatePlaceActionLink", HttpMethod.Post, url, null, new { uri = r.Uri, placeActionType = r.PlaceActionType, isPreferred = r.IsPreferred });
    }

    public async Task<Result<object>> UpdatePlaceActionLinkAsync(UpdatePlaceActionLinkRequest r)
    {
        var url = $"https://mybusinessplaceactions.googleapis.com/v1/locations/{r.LocationId}/placeActionLinks/{r.LinkId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdatePlaceActionLink", HttpMethod.Patch, url, query, new { uri = r.Uri, placeActionType = r.PlaceActionType, isPreferred = r.IsPreferred });
    }

    public async Task<Result<object>> DeletePlaceActionLinkAsync(DeletePlaceActionLinkRequest r)
    {
        var url = $"https://mybusinessplaceactions.googleapis.com/v1/locations/{r.LocationId}/placeActionLinks/{r.LinkId}";
        return await SendAsync(r.AccountKey, "DeletePlaceActionLink", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> ListPlaceActionTypesAsync(ListPlaceActionTypesRequest r)
    {
        var url = "https://mybusinessplaceactions.googleapis.com/v1/placeActionTypeMetadata";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "languageCode", r.LanguageCode);
        Add(query, "filter", r.Filter);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListPlaceActionTypes", HttpMethod.Get, url, query);
    }
}
