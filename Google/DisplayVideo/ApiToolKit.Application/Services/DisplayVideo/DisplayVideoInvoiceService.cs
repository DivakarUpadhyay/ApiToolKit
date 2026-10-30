using ApiToolKit.Application.DTOs.DisplayVideo.Invoice;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoInvoiceService : DisplayVideoServiceBase, IDisplayVideoInvoiceService
{
    public DisplayVideoInvoiceService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> LookupInvoiceCurrencyAsync(LookupInvoiceCurrencyRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/invoices:lookupInvoiceCurrency";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "invoiceMonth", r.InvoiceMonth);
        return await SendAsync(r.AccountKey, "LookupInvoiceCurrency", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> ListInvoicesAsync(ListInvoicesRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/invoices";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "loiSapinInvoiceType", r.LoiSapinInvoiceType);
        Add(query, "issueMonth", r.IssueMonth);
        return await SendAsync(r.AccountKey, "ListInvoices", HttpMethod.Get, url, query);
    }
}
