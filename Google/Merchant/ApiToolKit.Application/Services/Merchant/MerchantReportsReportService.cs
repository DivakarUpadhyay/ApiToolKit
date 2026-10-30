using ApiToolKit.Application.DTOs.Merchant.ReportsReport;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantReportsReportService : MerchantServiceBase, IMerchantReportsReportService
{
    public MerchantReportsReportService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> SearchReportAsync(SearchReportRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/reports/v1/accounts/{Esc(r.AccountId)}/reports:search";
        return await SendAsync(r.AccountKey, "SearchReport", HttpMethod.Post, url, null, r.Body);
    }
}
