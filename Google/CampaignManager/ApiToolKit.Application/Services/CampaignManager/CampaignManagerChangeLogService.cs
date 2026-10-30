using ApiToolKit.Application.DTOs.CampaignManager.ChangeLog;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerChangeLogService : CampaignManagerServiceBase, ICampaignManagerChangeLogService
{
    public CampaignManagerChangeLogService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetChangeLogAsync(GetChangeLogRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/changeLogs/{Esc(r.ChangeLogId)}";
        return await SendAsync(r.AccountKey, "GetChangeLog", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListChangeLogsAsync(ListChangeLogsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/changeLogs";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "maxChangeTime", r.MaxChangeTime);
        AddMany(query, "ids", r.Ids);
        AddMany(query, "objectIds", r.ObjectIds);
        Add(query, "minChangeTime", r.MinChangeTime);
        AddMany(query, "userProfileIds", r.UserProfileIds);
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "action", r.Action);
        Add(query, "pageToken", r.PageToken);
        Add(query, "searchString", r.SearchString);
        Add(query, "objectType", r.ObjectType);
        return await SendAsync(r.AccountKey, "ListChangeLogs", HttpMethod.Get, url, query);
    }
}
