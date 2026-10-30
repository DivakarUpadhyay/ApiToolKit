using ApiToolKit.Application.DTOs.CampaignManager.TargetingTemplate;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerTargetingTemplateService : CampaignManagerServiceBase, ICampaignManagerTargetingTemplateService
{
    public CampaignManagerTargetingTemplateService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> InsertTargetingTemplateAsync(InsertTargetingTemplateRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/targetingTemplates";
        return await SendAsync(r.AccountKey, "InsertTargetingTemplate", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateTargetingTemplateAsync(UpdateTargetingTemplateRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/targetingTemplates";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "UpdateTargetingTemplate", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetTargetingTemplateAsync(GetTargetingTemplateRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/targetingTemplates/{Esc(r.TargetingTemplateId)}";
        return await SendAsync(r.AccountKey, "GetTargetingTemplate", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateTargetingTemplateAltAsync(UpdateTargetingTemplateAltRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/targetingTemplates";
        return await SendAsync(r.AccountKey, "UpdateTargetingTemplateAlt", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> ListTargetingTemplatesAsync(ListTargetingTemplatesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/targetingTemplates";
        var query = new List<KeyValuePair<string, string?>>();
        AddMany(query, "ids", r.Ids);
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "searchString", r.SearchString);
        Add(query, "sortField", r.SortField);
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListTargetingTemplates", HttpMethod.Get, url, query);
    }
}
