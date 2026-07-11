using ApiToolKit.Application.DTOs.Shopee.PushNotification;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeePushNotificationService
{
    Task<Result<SetAppPushConfigResponse>> SetAppPushConfigAsync(SetAppPushConfigRequest request);
    Task<Result<GetAppPushConfigResponse>> GetAppPushConfigAsync(GetAppPushConfigRequest request);
}
