using ApiToolKit.Application.DTOs.CampaignManager.PlacementStrategy;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerPlacementStrategyService : CampaignManagerServiceBase, ICampaignManagerPlacementStrategyService
{
    public CampaignManagerPlacementStrategyService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListPlacementStrategiesAsync(ListPlacementStrategiesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placementStrategies";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "sortField", r.SortField);
        Add(query, "maxResults", r.MaxResults?.ToString());
        AddMany(query, "ids", r.Ids);
        Add(query, "pageToken", r.PageToken);
        Add(query, "searchString", r.SearchString);
        Add(query, "sortOrder", r.SortOrder);
        return await SendAsync(r.AccountKey, "ListPlacementStrategies", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> InsertPlacementStrategyAsync(InsertPlacementStrategyRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placementStrategies";
        return await SendAsync(r.AccountKey, "InsertPlacementStrategy", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdatePlacementStrategyAsync(UpdatePlacementStrategyRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placementStrategies";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "UpdatePlacementStrategy", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> DeletePlacementStrategyAsync(DeletePlacementStrategyRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placementStrategies/{Esc(r.PlacementStrategyId)}";
        return await SendAsync(r.AccountKey, "DeletePlacementStrategy", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> GetPlacementStrategyAsync(GetPlacementStrategyRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placementStrategies/{Esc(r.PlacementStrategyId)}";
        return await SendAsync(r.AccountKey, "GetPlacementStrategy", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdatePlacementStrategyAltAsync(UpdatePlacementStrategyAltRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placementStrategies";
        return await SendAsync(r.AccountKey, "UpdatePlacementStrategyAlt", HttpMethod.Put, url, null, r.Body);
    }
}
