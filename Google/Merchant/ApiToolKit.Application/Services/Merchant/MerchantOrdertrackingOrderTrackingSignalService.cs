using ApiToolKit.Application.DTOs.Merchant.OrdertrackingOrderTrackingSignal;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantOrdertrackingOrderTrackingSignalService : MerchantServiceBase, IMerchantOrdertrackingOrderTrackingSignalService
{
    public MerchantOrdertrackingOrderTrackingSignalService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> CreateOrderTrackingSignalAsync(CreateOrderTrackingSignalRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/ordertracking/v1/accounts/{Esc(r.AccountId)}/orderTrackingSignals";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "orderTrackingSignalId", r.OrderTrackingSignalId);
        return await SendAsync(r.AccountKey, "CreateOrderTrackingSignal", HttpMethod.Post, url, query, r.Body);
    }
}
