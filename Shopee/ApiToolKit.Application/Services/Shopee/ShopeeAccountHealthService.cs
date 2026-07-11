using ApiToolKit.Application.DTOs.Shopee.AccountHealth;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeAccountHealthService : IShopeeAccountHealthService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeeAccountHealthService(IShopeeAuthRepository authRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetToken(long shopId) { var t = await _authRepo.GetTokenByShopIdAsync(shopId); return t?.AccessToken; }

    public async Task<Result<GetShopPenaltyResponse>> GetShopPenaltyAsync(GetShopPenaltyRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<GetShopPenaltyResponse>.Fail("Access token not found.");
        var r = await _apiClient.GetAsync<GetShopPenaltyResponse>("/api/v2/account_health/get_shop_penalty", new Dictionary<string, string>(), token, request.ShopId);
        return r == null ? Result<GetShopPenaltyResponse>.Fail("No response from Shopee API.") : Result<GetShopPenaltyResponse>.Ok(r);
    }

    public async Task<Result<GetMetricListResponse>> GetMetricListAsync(GetMetricListRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<GetMetricListResponse>.Fail("Access token not found.");
        var q = new Dictionary<string, string> { ["metric_list"] = string.Join(",", request.metric_list) };
        var r = await _apiClient.GetAsync<GetMetricListResponse>("/api/v2/account_health/get_metric_list", q, token, request.ShopId);
        return r == null ? Result<GetMetricListResponse>.Fail("No response from Shopee API.") : Result<GetMetricListResponse>.Ok(r);
    }

    public async Task<Result<GetShopBriefPenaltyResponse>> GetShopBriefPenaltyAsync(GetShopBriefPenaltyRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<GetShopBriefPenaltyResponse>.Fail("Access token not found.");
        var r = await _apiClient.GetAsync<GetShopBriefPenaltyResponse>("/api/v2/account_health/get_shop_brief_penalty", new Dictionary<string, string>(), token, request.ShopId);
        return r == null ? Result<GetShopBriefPenaltyResponse>.Fail("No response from Shopee API.") : Result<GetShopBriefPenaltyResponse>.Ok(r);
    }
}
