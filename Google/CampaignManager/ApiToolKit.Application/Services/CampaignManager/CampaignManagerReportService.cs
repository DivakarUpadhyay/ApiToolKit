using ApiToolKit.Application.DTOs.CampaignManager.Report;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerReportService : CampaignManagerServiceBase, ICampaignManagerReportService
{
    public CampaignManagerReportService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> RunReportAsync(RunReportRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.ProfileId)}/reports/{Esc(r.ReportId)}/run";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "synchronous", r.Synchronous?.ToString().ToLower());
        return await SendAsync(r.AccountKey, "RunReport", HttpMethod.Post, url, query);
    }

    public async Task<Result<object>> DeleteReportAsync(DeleteReportRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.ProfileId)}/reports/{Esc(r.ReportId)}";
        return await SendAsync(r.AccountKey, "DeleteReport", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> ListReportsAsync(ListReportsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.ProfileId)}/reports";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "pageToken", r.PageToken);
        Add(query, "scope", r.Scope);
        Add(query, "sortField", r.SortField);
        return await SendAsync(r.AccountKey, "ListReports", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetReportAsync(GetReportRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.ProfileId)}/reports/{Esc(r.ReportId)}";
        return await SendAsync(r.AccountKey, "GetReport", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateReportAsync(UpdateReportRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.ProfileId)}/reports/{Esc(r.ReportId)}";
        return await SendAsync(r.AccountKey, "UpdateReport", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> InsertReportAsync(InsertReportRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.ProfileId)}/reports";
        return await SendAsync(r.AccountKey, "InsertReport", HttpMethod.Post, url, null, r.Body);
    }
}
