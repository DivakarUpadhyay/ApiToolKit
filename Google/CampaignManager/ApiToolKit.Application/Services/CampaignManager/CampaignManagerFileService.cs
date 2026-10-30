using ApiToolKit.Application.DTOs.CampaignManager.File;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerFileService : CampaignManagerServiceBase, ICampaignManagerFileService
{
    public CampaignManagerFileService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListFilesAsync(ListFilesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.ProfileId)}/files";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "pageToken", r.PageToken);
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "scope", r.Scope);
        Add(query, "sortField", r.SortField);
        return await SendAsync(r.AccountKey, "ListFiles", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetFileAsync(GetFileRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/reports/{Esc(r.ReportId)}/files/{Esc(r.FileId)}";
        return await SendAsync(r.AccountKey, "GetFile", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetFileContentAsync(GetFileContentRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/reports/{Esc(r.ReportId)}/files/{Esc(r.FileId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "alt", "media");
        return await SendAsync(r.AccountKey, "GetFileContent", HttpMethod.Get, url, query, null, raw: true);
    }
}
