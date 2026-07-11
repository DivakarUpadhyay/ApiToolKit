using ApiToolKit.Application.DTOs.Shopee.PushNotification;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeePushNotificationService : IShopeePushNotificationService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeePushNotificationService(IShopeeAuthRepository authRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetToken(long shopId) { var t = await _authRepo.GetTokenByShopIdAsync(shopId); return t?.AccessToken; }

    public async Task<Result<SetAppPushConfigResponse>> SetAppPushConfigAsync(SetAppPushConfigRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<SetAppPushConfigResponse>.Fail("Access token not found.");
        var body = new { callback_url = request.callback_url, push_type = request.push_type, blocked_shop_id_list = request.blocked_shop_id_list };
        var r = await _apiClient.PostAsync<SetAppPushConfigResponse>("/api/v2/push/set_app_push_config", body, token, request.ShopId);
        return r == null ? Result<SetAppPushConfigResponse>.Fail("No response from Shopee API.") : Result<SetAppPushConfigResponse>.Ok(r);
    }

    public async Task<Result<GetAppPushConfigResponse>> GetAppPushConfigAsync(GetAppPushConfigRequest request)
    {
        var token = await GetToken(request.ShopId); if (token == null) return Result<GetAppPushConfigResponse>.Fail("Access token not found.");
        var r = await _apiClient.GetAsync<GetAppPushConfigResponse>("/api/v2/push/get_app_push_config", new Dictionary<string, string>(), token, request.ShopId);
        return r == null ? Result<GetAppPushConfigResponse>.Fail("No response from Shopee API.") : Result<GetAppPushConfigResponse>.Ok(r);
    }
}
