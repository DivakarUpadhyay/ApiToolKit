using ApiToolKit.Application.DTOs.Shopee.Voucher;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeVoucherService : IShopeeVoucherService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeVoucherRepository _voucherRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeeVoucherService(IShopeeAuthRepository authRepo, IShopeeVoucherRepository voucherRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _voucherRepo = voucherRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetAccessTokenAsync(long shopId)
    {
        var token = await _authRepo.GetTokenByShopIdAsync(shopId);
        return token?.AccessToken;
    }

    public async Task<Result<VoucherResponse>> CreateVoucherAsync(CreateVoucherRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<VoucherResponse>.Fail("Access token not found.");

        var body = new { voucher_code = request.VoucherCode, voucher_type = request.VoucherType, discount_type = request.DiscountType, reward_value = request.RewardValue, minimum_spend = request.MinimumSpend, start_time = request.StartTime, end_time = request.EndTime, usage_quantity = request.UsageQuantity };
        var response = await _apiClient.PostAsync<VoucherResponse>("/api/v2/voucher/add_voucher", body, accessToken, request.ShopId);
        if (response == null) return Result<VoucherResponse>.Fail("No response from Shopee API.");
        return Result<VoucherResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteVoucherAsync(DeleteVoucherRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { voucher_id = request.VoucherId };
        await _apiClient.PostAsync<dynamic>("/api/v2/voucher/delete_voucher", body, accessToken, request.ShopId);
        await _voucherRepo.DeleteVoucherAsync(request.ShopId, request.VoucherId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<VoucherResponse>> UpdateVoucherAsync(UpdateVoucherRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<VoucherResponse>.Fail("Access token not found.");

        var body = new { voucher_id = request.VoucherId, voucher_code = request.VoucherCode, reward_value = request.RewardValue, minimum_spend = request.MinimumSpend, start_time = request.StartTime, end_time = request.EndTime, usage_quantity = request.UsageQuantity };
        var response = await _apiClient.PostAsync<VoucherResponse>("/api/v2/voucher/update_voucher", body, accessToken, request.ShopId);
        if (response == null) return Result<VoucherResponse>.Fail("No response from Shopee API.");
        return Result<VoucherResponse>.Ok(response);
    }

    public async Task<Result<bool>> EndVoucherAsync(EndVoucherRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { voucher_id = request.VoucherId };
        await _apiClient.PostAsync<dynamic>("/api/v2/voucher/end_voucher", body, accessToken, request.ShopId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<VoucherResponse>> GetVoucherAsync(GetVoucherRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<VoucherResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["voucher_id"] = request.VoucherId.ToString() };
        var response = await _apiClient.GetAsync<VoucherResponse>("/api/v2/voucher/get_voucher", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<VoucherResponse>.Fail("No response from Shopee API.");
        return Result<VoucherResponse>.Ok(response);
    }

    public async Task<Result<VoucherListResponse>> GetVoucherListAsync(GetVoucherListRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<VoucherListResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["status"] = request.Status.ToString(), ["page_no"] = request.PageNo.ToString(), ["page_size"] = request.PageSize.ToString() };
        var response = await _apiClient.GetAsync<VoucherListResponse>("/api/v2/voucher/get_voucher_list", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<VoucherListResponse>.Fail("No response from Shopee API.");
        return Result<VoucherListResponse>.Ok(response);
    }
}


