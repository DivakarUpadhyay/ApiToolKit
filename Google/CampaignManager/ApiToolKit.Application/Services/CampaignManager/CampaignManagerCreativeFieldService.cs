using ApiToolKit.Application.DTOs.CampaignManager.CreativeField;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerCreativeFieldService : CampaignManagerServiceBase, ICampaignManagerCreativeFieldService
{
    public CampaignManagerCreativeFieldService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> UpdateCreativeFieldAsync(UpdateCreativeFieldRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeFields";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "UpdateCreativeField", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetCreativeFieldAsync(GetCreativeFieldRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeFields/{Esc(r.CreativeFieldId)}";
        return await SendAsync(r.AccountKey, "GetCreativeField", HttpMethod.Get, url);
    }

    public async Task<Result<object>> InsertCreativeFieldAsync(InsertCreativeFieldRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeFields";
        return await SendAsync(r.AccountKey, "InsertCreativeField", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateCreativeFieldAltAsync(UpdateCreativeFieldAltRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeFields";
        return await SendAsync(r.AccountKey, "UpdateCreativeFieldAlt", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> DeleteCreativeFieldAsync(DeleteCreativeFieldRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeFields/{Esc(r.CreativeFieldId)}";
        return await SendAsync(r.AccountKey, "DeleteCreativeField", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> ListCreativeFieldsAsync(ListCreativeFieldsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeFields";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "sortOrder", r.SortOrder);
        AddMany(query, "ids", r.Ids);
        Add(query, "searchString", r.SearchString);
        Add(query, "pageToken", r.PageToken);
        Add(query, "maxResults", r.MaxResults?.ToString());
        AddMany(query, "advertiserIds", r.AdvertiserIds);
        Add(query, "sortField", r.SortField);
        return await SendAsync(r.AccountKey, "ListCreativeFields", HttpMethod.Get, url, query);
    }
}
