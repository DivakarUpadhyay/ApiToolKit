using ApiToolKit.Application.DTOs.CampaignManager.AdvertiserInvoice;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerAdvertiserInvoiceService : CampaignManagerServiceBase, ICampaignManagerAdvertiserInvoiceService
{
    public CampaignManagerAdvertiserInvoiceService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListAdvertiserInvoicesAsync(ListAdvertiserInvoicesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertisers/{Esc(r.AdvertiserId)}/invoices";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "issueMonth", r.IssueMonth);
        return await SendAsync(r.AccountKey, "ListAdvertiserInvoices", HttpMethod.Get, url, query);
    }
}
