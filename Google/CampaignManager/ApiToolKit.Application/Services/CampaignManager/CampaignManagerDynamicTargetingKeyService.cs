using ApiToolKit.Application.DTOs.CampaignManager.DynamicTargetingKey;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerDynamicTargetingKeyService : CampaignManagerServiceBase, ICampaignManagerDynamicTargetingKeyService
{
    public CampaignManagerDynamicTargetingKeyService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> DeleteDynamicTargetingKeyAsync(DeleteDynamicTargetingKeyRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/dynamicTargetingKeys/{Esc(r.DynamicTargetingKeyId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "name", r.Name);
        Add(query, "objectType", r.ObjectType);
        return await SendAsync(r.AccountKey, "DeleteDynamicTargetingKey", HttpMethod.Delete, url, query);
    }

    public async Task<Result<object>> InsertDynamicTargetingKeyAsync(InsertDynamicTargetingKeyRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/dynamicTargetingKeys";
        return await SendAsync(r.AccountKey, "InsertDynamicTargetingKey", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListDynamicTargetingKeysAsync(ListDynamicTargetingKeysRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/dynamicTargetingKeys";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "objectId", r.ObjectId);
        AddMany(query, "names", r.Names);
        Add(query, "objectType", r.ObjectType);
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "ListDynamicTargetingKeys", HttpMethod.Get, url, query);
    }
}
