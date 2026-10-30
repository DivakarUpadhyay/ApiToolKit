using ApiToolKit.Application.DTOs.Merchant.NotificationsNotificationsubscription;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantNotificationsNotificationsubscriptionService
{
    Task<Result<object>> UpdateNotificationsubscriptionAsync(UpdateNotificationsubscriptionRequest request);
    Task<Result<object>> CreateNotificationsubscriptionAsync(CreateNotificationsubscriptionRequest request);
    Task<Result<object>> DeleteNotificationsubscriptionAsync(DeleteNotificationsubscriptionRequest request);
    Task<Result<object>> GetNotificationsubscriptionAsync(GetNotificationsubscriptionRequest request);
    Task<Result<object>> ListNotificationsubscriptionsAsync(ListNotificationsubscriptionsRequest request);
}
