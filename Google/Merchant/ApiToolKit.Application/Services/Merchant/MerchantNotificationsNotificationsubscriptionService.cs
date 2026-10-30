using ApiToolKit.Application.DTOs.Merchant.NotificationsNotificationsubscription;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantNotificationsNotificationsubscriptionService : MerchantServiceBase, IMerchantNotificationsNotificationsubscriptionService
{
    public MerchantNotificationsNotificationsubscriptionService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> UpdateNotificationsubscriptionAsync(UpdateNotificationsubscriptionRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/notifications/v1/accounts/{Esc(r.AccountId)}/notificationsubscriptions/{Esc(r.NotificationsubscriptionId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateNotificationsubscription", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> CreateNotificationsubscriptionAsync(CreateNotificationsubscriptionRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/notifications/v1/accounts/{Esc(r.AccountId)}/notificationsubscriptions";
        return await SendAsync(r.AccountKey, "CreateNotificationsubscription", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> DeleteNotificationsubscriptionAsync(DeleteNotificationsubscriptionRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/notifications/v1/accounts/{Esc(r.AccountId)}/notificationsubscriptions/{Esc(r.NotificationsubscriptionId)}";
        return await SendAsync(r.AccountKey, "DeleteNotificationsubscription", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> GetNotificationsubscriptionAsync(GetNotificationsubscriptionRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/notifications/v1/accounts/{Esc(r.AccountId)}/notificationsubscriptions/{Esc(r.NotificationsubscriptionId)}";
        return await SendAsync(r.AccountKey, "GetNotificationsubscription", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListNotificationsubscriptionsAsync(ListNotificationsubscriptionsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/notifications/v1/accounts/{Esc(r.AccountId)}/notificationsubscriptions";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        return await SendAsync(r.AccountKey, "ListNotificationsubscriptions", HttpMethod.Get, url, query);
    }
}
