using ApiToolKit.Application.DTOs.Facebook.Webhook;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookWebhookService
{
    Task<Result<SubscribeWebhookResponse>> SubscribeWebhookAsync(SubscribeWebhookRequest request, string accessToken);
    Task<Result<GetWebhookSubscriptionsResponse>> GetWebhookSubscriptionsAsync(GetWebhookSubscriptionsRequest request, string accessToken);
    Task<Result<bool>> DeleteWebhookAsync(DeleteWebhookRequest request, string accessToken);
    Task<Result<string>> VerifyWebhookAsync(WebhookVerificationRequest request);
    Task<Result<bool>> ProcessWebhookPayloadAsync(WebhookPayload payload);
}
