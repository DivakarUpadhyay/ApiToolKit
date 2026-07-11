using ApiToolKit.Application.DTOs.Facebook.Webhook;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookWebhookService : IFacebookWebhookService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookWebhookRepository _webhookRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookWebhookService(IFacebookAuthRepository authRepo, IFacebookWebhookRepository webhookRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _webhookRepo = webhookRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<SubscribeWebhookResponse>> SubscribeWebhookAsync(SubscribeWebhookRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<SubscribeWebhookResponse>.Fail("Facebook configuration not found.");

        var body = new
        {
            @object = request.ObjectType,
            callback_url = request.CallbackUrl,
            verify_token = request.VerifyToken,
            fields = request.Fields
        };

        var response = await _apiClient.PostAsync<dynamic>($"/{config.AppId}/subscriptions", body, accessToken);
        if (response == null) return Result<SubscribeWebhookResponse>.Fail("No response from Facebook API.");

        bool success = response?.success == true;

        await _webhookRepo.SaveWebhookAsync(new FacebookWebhook
        {
            ObjectType = request.ObjectType,
            Fields = string.Join(",", request.Fields),
            CallbackUrl = request.CallbackUrl,
            VerifyToken = request.VerifyToken,
            IsActive = success,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _webhookRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "SubscribeWebhook",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = success,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<SubscribeWebhookResponse>.Ok(new SubscribeWebhookResponse { Success = success });
    }

    public async Task<Result<GetWebhookSubscriptionsResponse>> GetWebhookSubscriptionsAsync(GetWebhookSubscriptionsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetWebhookSubscriptionsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string> { ["access_token"] = accessToken };
        var response = await _apiClient.GetAsync<dynamic>($"/{request.AppId}/subscriptions", queryParams);
        if (response == null) return Result<GetWebhookSubscriptionsResponse>.Fail("No response from Facebook API.");

        var subscriptions = JsonConvert.DeserializeObject<List<WebhookSubscriptionResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<WebhookSubscriptionResponse>();

        return Result<GetWebhookSubscriptionsResponse>.Ok(new GetWebhookSubscriptionsResponse { Data = subscriptions });
    }

    public async Task<Result<bool>> DeleteWebhookAsync(DeleteWebhookRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["object"] = request.ObjectType,
            ["access_token"] = accessToken
        };
        if (request.Fields?.Count > 0) queryParams["fields"] = string.Join(",", request.Fields);

        var response = await _apiClient.DeleteAsync<dynamic>($"/{config.AppId}/subscriptions", accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(response?.success == true);
    }

    public async Task<Result<string>> VerifyWebhookAsync(WebhookVerificationRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<string>.Fail("Facebook configuration not found.");

        if (request.Mode != "subscribe")
            return Result<string>.Fail("Invalid mode.");

        var webhooks = await _webhookRepo.GetWebhooksByObjectTypeAsync("page");
        var matchingWebhook = webhooks.FirstOrDefault(w => w.VerifyToken == request.Token);

        if (matchingWebhook == null)
            return Result<string>.Fail("Verify token mismatch.");

        return Result<string>.Ok(request.Challenge ?? string.Empty);
    }

    public async Task<Result<bool>> ProcessWebhookPayloadAsync(WebhookPayload payload)
    {
        try
        {
            await _webhookRepo.SaveApiLogAsync(new FacebookApiLog
            {
                ApiName = "ProcessWebhookPayload",
                RequestBody = JsonConvert.SerializeObject(payload),
                IsSuccess = true,
                StatusCode = 200,
                CreatedAt = DateTime.UtcNow
            });

            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            await _webhookRepo.SaveApiLogAsync(new FacebookApiLog
            {
                ApiName = "ProcessWebhookPayload",
                RequestBody = JsonConvert.SerializeObject(payload),
                ResponseBody = ex.Message,
                IsSuccess = false,
                StatusCode = 500,
                CreatedAt = DateTime.UtcNow
            });

            return Result<bool>.Fail($"Failed to process webhook payload: {ex.Message}");
        }
    }
}
