using ApiToolKit.Application.DTOs.CampaignManager.CreativeFieldValue;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerCreativeFieldValueService : CampaignManagerServiceBase, ICampaignManagerCreativeFieldValueService
{
    public CampaignManagerCreativeFieldValueService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetCreativeFieldValueAsync(GetCreativeFieldValueRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeFields/{Esc(r.CreativeFieldId)}/creativeFieldValues/{Esc(r.CreativeFieldValueId)}";
        return await SendAsync(r.AccountKey, "GetCreativeFieldValue", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateCreativeFieldValueAsync(UpdateCreativeFieldValueRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeFields/{Esc(r.CreativeFieldId)}/creativeFieldValues";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "UpdateCreativeFieldValue", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> InsertCreativeFieldValueAsync(InsertCreativeFieldValueRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeFields/{Esc(r.CreativeFieldId)}/creativeFieldValues";
        return await SendAsync(r.AccountKey, "InsertCreativeFieldValue", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateCreativeFieldValueAltAsync(UpdateCreativeFieldValueAltRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeFields/{Esc(r.CreativeFieldId)}/creativeFieldValues";
        return await SendAsync(r.AccountKey, "UpdateCreativeFieldValueAlt", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> ListCreativeFieldValuesAsync(ListCreativeFieldValuesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeFields/{Esc(r.CreativeFieldId)}/creativeFieldValues";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "searchString", r.SearchString);
        Add(query, "pageToken", r.PageToken);
        Add(query, "maxResults", r.MaxResults?.ToString());
        AddMany(query, "ids", r.Ids);
        Add(query, "sortField", r.SortField);
        return await SendAsync(r.AccountKey, "ListCreativeFieldValues", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> DeleteCreativeFieldValueAsync(DeleteCreativeFieldValueRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeFields/{Esc(r.CreativeFieldId)}/creativeFieldValues/{Esc(r.CreativeFieldValueId)}";
        return await SendAsync(r.AccountKey, "DeleteCreativeFieldValue", HttpMethod.Delete, url);
    }
}
