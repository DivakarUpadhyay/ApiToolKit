using ApiToolKit.Application.DTOs.Shopee.Payment;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
namespace ApiToolKit.Application.Services.Shopee;
public class ShopeePaymentService : IShopeePaymentService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeApiClient _apiClient;
    public ShopeePaymentService(IShopeeAuthRepository authRepo, IShopeeApiClient apiClient) { _authRepo = authRepo; _apiClient = apiClient; }
    private async Task<string?> GetToken(long shopId) { var t = await _authRepo.GetTokenByShopIdAsync(shopId); return t?.AccessToken; }
    public async Task<Result<object>> GetEscrowListAsync(GetEscrowListRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var q = new Dictionary<string, string> { ["page_no"] = request.PageNo.ToString(), ["page_size"] = request.PageSize.ToString() };
        if (request.ReleaseTimeFrom > 0) q["release_time_from"] = request.ReleaseTimeFrom.ToString();
        if (request.ReleaseTimeTo > 0) q["release_time_to"] = request.ReleaseTimeTo.ToString();
        var r = await _apiClient.GetAsync<object>("/api/v2/payment/get_escrow_list", q, token, request.ShopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
    public async Task<Result<object>> GetEscrowDetailAsync(GetEscrowDetailRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var r = await _apiClient.GetAsync<object>("/api/v2/payment/get_escrow_detail", new Dictionary<string, string> { ["order_sn"] = request.OrderSn }, token, request.ShopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
    public async Task<Result<object>> GetEscrowDetailBatchAsync(long shopId, List<string> orderSnList) {
        var token = await GetToken(shopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var r = await _apiClient.PostAsync<object>("/api/v2/payment/get_escrow_detail_batch", new { order_sn_list = orderSnList }, token, shopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
    public async Task<Result<object>> GetWalletTransactionListAsync(GetWalletTransactionListRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var q = new Dictionary<string, string> { ["page_no"] = request.PageNo.ToString(), ["page_size"] = request.PageSize.ToString() };
        if (request.CreateTimeFrom > 0) q["create_time_from"] = request.CreateTimeFrom.ToString();
        if (request.CreateTimeTo > 0) q["create_time_to"] = request.CreateTimeTo.ToString();
        if (!string.IsNullOrEmpty(request.WalletType)) q["wallet_type"] = request.WalletType;
        var r = await _apiClient.GetAsync<object>("/api/v2/payment/get_wallet_transaction_list", q, token, request.ShopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
    public async Task<Result<object>> GetPaymentMethodListAsync(GetPaymentMethodListRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var r = await _apiClient.GetAsync<object>("/api/v2/payment/get_payment_method_list", new Dictionary<string, string>(), token, request.ShopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
    public async Task<Result<object>> GetShopInstallmentStatusAsync(GetInstallmentStatusRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var r = await _apiClient.GetAsync<object>("/api/v2/payment/get_shop_installment_status", new Dictionary<string, string>(), token, request.ShopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
    public async Task<Result<bool>> SetShopInstallmentStatusAsync(SetInstallmentStatusRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<bool>.Fail("Access token not found.");
        await _apiClient.PostAsync<dynamic>("/api/v2/payment/set_shop_installment_status", new { installment_status = request.InstallmentStatus ? 1 : 0 }, token, request.ShopId);
        return Result<bool>.Ok(true);
    }
    public async Task<Result<object>> GetItemInstallmentStatusAsync(GetItemInstallmentRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var r = await _apiClient.PostAsync<object>("/api/v2/payment/get_item_installment_status", new { item_id_list = request.ItemIdList }, token, request.ShopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
    public async Task<Result<bool>> SetItemInstallmentStatusAsync(SetItemInstallmentRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<bool>.Fail("Access token not found.");
        await _apiClient.PostAsync<dynamic>("/api/v2/payment/set_item_installment_status", new { item_id = request.ItemId, installment_status = request.InstallmentStatus ? 1 : 0 }, token, request.ShopId);
        return Result<bool>.Ok(true);
    }
    public async Task<Result<bool>> GenerateIncomeReportAsync(GenerateIncomeReportRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<bool>.Fail("Access token not found.");
        await _apiClient.PostAsync<dynamic>("/api/v2/payment/generate_income_report", new { start_date = request.StartDate, end_date = request.EndDate }, token, request.ShopId);
        return Result<bool>.Ok(true);
    }
    public async Task<Result<object>> GetIncomeReportAsync(GetIncomeReportRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var q = new Dictionary<string, string> { ["report_type"] = request.ReportType, ["start_date"] = request.StartDate, ["end_date"] = request.EndDate };
        var r = await _apiClient.GetAsync<object>("/api/v2/payment/get_income_report", q, token, request.ShopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
    public async Task<Result<object>> GetBillingTransactionInfoAsync(GetBillingTransactionRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var q = new Dictionary<string, string> { ["page_no"] = request.PageNo.ToString(), ["page_size"] = request.PageSize.ToString() };
        if (request.CreateTimeFrom > 0) q["create_time_from"] = request.CreateTimeFrom.ToString();
        if (request.CreateTimeTo > 0) q["create_time_to"] = request.CreateTimeTo.ToString();
        var r = await _apiClient.GetAsync<object>("/api/v2/payment/get_billing_transaction_info", q, token, request.ShopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
    public async Task<Result<object>> GetPayoutDetailAsync(GetPayoutDetailRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var q = new Dictionary<string, string> { ["page_no"] = request.PageNo.ToString(), ["page_size"] = request.PageSize.ToString() };
        var r = await _apiClient.GetAsync<object>("/api/v2/payment/get_payout_detail", q, token, request.ShopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
    public async Task<Result<object>> GetPayoutInfoAsync(GetPayoutInfoRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var q = new Dictionary<string, string> { ["page_no"] = request.PageNo.ToString(), ["page_size"] = request.PageSize.ToString() };
        var r = await _apiClient.GetAsync<object>("/api/v2/payment/get_payout_info", q, token, request.ShopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
}
