using ApiToolKit.Application.DTOs.YouTube.Subscription;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.YouTube;

public interface IYouTubeSubscriptionService
{
    Task<Result<SubscriptionListResponse>> ListSubscriptionsAsync(ListSubscriptionsRequest request, string accessToken);
    Task<Result<SubscriptionResponse>> SubscribeAsync(SubscribeRequest request, string accessToken);
    Task<Result<bool>> DeleteSubscriptionAsync(DeleteSubscriptionRequest request, string accessToken);
}
