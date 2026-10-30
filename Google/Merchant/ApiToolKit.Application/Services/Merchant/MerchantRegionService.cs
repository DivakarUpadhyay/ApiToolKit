using ApiToolKit.Application.DTOs.Merchant.Region;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantRegionService : MerchantServiceBase, IMerchantRegionService
{
    public MerchantRegionService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> DeleteRegionAsync(DeleteRegionRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/regions/{Esc(r.RegionId)}";
        return await SendAsync(r.AccountKey, "DeleteRegion", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> BatchCreateRegionsAsync(BatchCreateRegionsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/regions:batchCreate";
        return await SendAsync(r.AccountKey, "BatchCreateRegions", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateRegionAsync(UpdateRegionRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/regions/{Esc(r.RegionId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateRegion", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> ListRegionsAsync(ListRegionsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/regions";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListRegions", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> BatchUpdateRegionsAsync(BatchUpdateRegionsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/regions:batchUpdate";
        return await SendAsync(r.AccountKey, "BatchUpdateRegions", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> BatchDeleteRegionsAsync(BatchDeleteRegionsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/regions:batchDelete";
        return await SendAsync(r.AccountKey, "BatchDeleteRegions", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GetRegionAsync(GetRegionRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/regions/{Esc(r.RegionId)}";
        return await SendAsync(r.AccountKey, "GetRegion", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateRegionAsync(CreateRegionRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/regions";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "regionId", r.RegionId);
        return await SendAsync(r.AccountKey, "CreateRegion", HttpMethod.Post, url, query, r.Body);
    }
}
