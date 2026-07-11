using ApiToolKit.Application.DTOs.Firebase.Fcm;
using ApiToolKit.Application.Interfaces.Services.Firebase;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Firebase;
using ApiToolKit.Domain.Interfaces.Repositories.Firebase;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Firebase;

public class FcmService : IFcmService
{
    private readonly IFirebaseApiClient _apiClient;
    private readonly IFcmRepository _fcmRepo;
    private readonly IFirebaseAuthRepository _authRepo;

    public FcmService(IFirebaseApiClient apiClient, IFcmRepository fcmRepo, IFirebaseAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _fcmRepo = fcmRepo;
        _authRepo = authRepo;
    }

    private string FcmUrl(string projectId) =>
        $"https://fcm.googleapis.com/v1/projects/{projectId}/messages:send";

    private string IidUrl(string projectId, string action) =>
        $"https://iid.googleapis.com/{action}";

    public async Task<Result<FcmMessageResponse>> SendToTokenAsync(SendToTokenRequest request, string accessToken)
    {
        try
        {
            var body = new
            {
                message = new
                {
                    token = request.Token,
                    notification = new FcmNotification { Title = request.Title, Body = request.Body, Image = request.ImageUrl },
                    data = request.Data,
                    android = request.Android,
                    apns = request.Apns,
                    webpush = request.Webpush
                }
            };

            var response = await _apiClient.PostAsync<FcmMessageResponse>(FcmUrl(request.ProjectId), body, accessToken);
            if (response == null) return Result<FcmMessageResponse>.Fail("No response from FCM API.");

            await _fcmRepo.SaveMessageAsync(new FcmMessage
            {
                MessageId = response.Name,
                ProjectId = request.ProjectId,
                Title = request.Title,
                Body = request.Body,
                Token = request.Token,
                Data = request.Data != null ? JsonConvert.SerializeObject(request.Data) : null,
                Platform = "token",
                SendStatus = "sent",
                SentAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            });

            return Result<FcmMessageResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<FcmMessageResponse>.Fail($"SendToToken failed: {ex.Message}");
        }
    }

    public async Task<Result<FcmMessageResponse>> SendToTopicAsync(SendToTopicRequest request, string accessToken)
    {
        try
        {
            var body = new
            {
                message = new
                {
                    topic = request.Topic,
                    notification = new FcmNotification { Title = request.Title, Body = request.Body, Image = request.ImageUrl },
                    data = request.Data,
                    android = request.Android,
                    apns = request.Apns,
                    webpush = request.Webpush
                }
            };

            var response = await _apiClient.PostAsync<FcmMessageResponse>(FcmUrl(request.ProjectId), body, accessToken);
            if (response == null) return Result<FcmMessageResponse>.Fail("No response from FCM API.");

            await _fcmRepo.SaveMessageAsync(new FcmMessage
            {
                MessageId = response.Name,
                ProjectId = request.ProjectId,
                Title = request.Title,
                Body = request.Body,
                Topic = request.Topic,
                Data = request.Data != null ? JsonConvert.SerializeObject(request.Data) : null,
                Platform = "topic",
                SendStatus = "sent",
                SentAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            });

            return Result<FcmMessageResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<FcmMessageResponse>.Fail($"SendToTopic failed: {ex.Message}");
        }
    }

    public async Task<Result<FcmMessageResponse>> SendToConditionAsync(SendToConditionRequest request, string accessToken)
    {
        try
        {
            var body = new
            {
                message = new
                {
                    condition = request.Condition,
                    notification = new FcmNotification { Title = request.Title, Body = request.Body },
                    data = request.Data
                }
            };

            var response = await _apiClient.PostAsync<FcmMessageResponse>(FcmUrl(request.ProjectId), body, accessToken);
            if (response == null) return Result<FcmMessageResponse>.Fail("No response from FCM API.");

            await _fcmRepo.SaveMessageAsync(new FcmMessage
            {
                MessageId = response.Name,
                ProjectId = request.ProjectId,
                Title = request.Title,
                Body = request.Body,
                Condition = request.Condition,
                Data = request.Data != null ? JsonConvert.SerializeObject(request.Data) : null,
                Platform = "condition",
                SendStatus = "sent",
                SentAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            });

            return Result<FcmMessageResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<FcmMessageResponse>.Fail($"SendToCondition failed: {ex.Message}");
        }
    }

    public async Task<Result<MulticastResponse>> SendMulticastAsync(MulticastRequest request, string accessToken)
    {
        try
        {
            if (request.Tokens.Count > 500)
                return Result<MulticastResponse>.Fail("Multicast supports a maximum of 500 tokens.");

            var results = new List<MulticastSingleResponse>();
            int successCount = 0;
            int failureCount = 0;

            foreach (var token in request.Tokens)
            {
                var body = new
                {
                    message = new
                    {
                        token,
                        notification = new FcmNotification { Title = request.Title, Body = request.Body, Image = request.ImageUrl },
                        data = request.Data
                    }
                };

                var response = await _apiClient.PostAsync<FcmMessageResponse>(FcmUrl(request.ProjectId), body, accessToken);
                if (response?.Name != null)
                {
                    results.Add(new MulticastSingleResponse { MessageId = response.Name });
                    successCount++;
                }
                else
                {
                    results.Add(new MulticastSingleResponse { Error = new MulticastError { Status = "ERROR", Message = "Failed to send" } });
                    failureCount++;
                }
            }

            return Result<MulticastResponse>.Ok(new MulticastResponse
            {
                SuccessCount = successCount,
                FailureCount = failureCount,
                Responses = results
            });
        }
        catch (Exception ex)
        {
            return Result<MulticastResponse>.Fail($"SendMulticast failed: {ex.Message}");
        }
    }

    public async Task<Result<SubscribeToTopicResponse>> SubscribeToTopicAsync(SubscribeToTopicRequest request, string accessToken)
    {
        try
        {
            var body = new
            {
                registration_tokens = request.RegistrationTokens,
                to = $"/topics/{request.Topic}"
            };

            var response = await _apiClient.PostAsync<SubscribeToTopicResponse>(
                "https://iid.googleapis.com/iid/v1:batchAdd", body, accessToken);
            if (response == null) return Result<SubscribeToTopicResponse>.Fail("No response from IID API.");

            response.SuccessCount = response.Results?.Count(r => r.Error == null) ?? 0;
            response.FailureCount = response.Results?.Count(r => r.Error != null) ?? 0;

            return Result<SubscribeToTopicResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<SubscribeToTopicResponse>.Fail($"SubscribeToTopic failed: {ex.Message}");
        }
    }

    public async Task<Result<SubscribeToTopicResponse>> UnsubscribeFromTopicAsync(UnsubscribeFromTopicRequest request, string accessToken)
    {
        try
        {
            var body = new
            {
                registration_tokens = request.RegistrationTokens,
                to = $"/topics/{request.Topic}"
            };

            var response = await _apiClient.PostAsync<SubscribeToTopicResponse>(
                "https://iid.googleapis.com/iid/v1:batchRemove", body, accessToken);
            if (response == null) return Result<SubscribeToTopicResponse>.Fail("No response from IID API.");

            response.SuccessCount = response.Results?.Count(r => r.Error == null) ?? 0;
            response.FailureCount = response.Results?.Count(r => r.Error != null) ?? 0;

            return Result<SubscribeToTopicResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<SubscribeToTopicResponse>.Fail($"UnsubscribeFromTopic failed: {ex.Message}");
        }
    }
}
