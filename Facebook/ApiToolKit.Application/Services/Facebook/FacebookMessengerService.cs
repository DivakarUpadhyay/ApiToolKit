using ApiToolKit.Application.DTOs.Facebook.Messenger;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookMessengerService : IFacebookMessengerService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookMessengerRepository _messengerRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookMessengerService(IFacebookAuthRepository authRepo, IFacebookMessengerRepository messengerRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _messengerRepo = messengerRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<SendMessageResponse>> SendMessageAsync(SendMessageRequest request, string pageAccessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<SendMessageResponse>.Fail("Facebook configuration not found.");

        object messagePayload;
        if (request.QuickReplies?.Count > 0)
        {
            messagePayload = new
            {
                text = request.MessageText,
                quick_replies = request.QuickReplies.Select(q => new
                {
                    content_type = q.ContentType,
                    title = q.Title,
                    payload = q.Payload,
                    image_url = q.ImageUrl
                })
            };
        }
        else if (request.AttachmentType != null && request.AttachmentUrl != null)
        {
            messagePayload = new
            {
                attachment = new
                {
                    type = request.AttachmentType,
                    payload = new { url = request.AttachmentUrl, is_reusable = true }
                }
            };
        }
        else
        {
            messagePayload = new { text = request.MessageText };
        }

        var body = new
        {
            recipient = new { id = request.RecipientId },
            message = messagePayload,
            messaging_type = "RESPONSE"
        };

        var response = await _apiClient.PostAsync<dynamic>($"/me/messages", body, pageAccessToken);
        if (response == null) return Result<SendMessageResponse>.Fail("No response from Facebook API.");

        string recipientId = response?.recipient_id?.ToString() ?? string.Empty;
        string messageId = response?.message_id?.ToString() ?? string.Empty;

        await _messengerRepo.SaveMessageAsync(new FacebookMessenger
        {
            RecipientId = request.RecipientId,
            MessageText = request.MessageText,
            MessageId = messageId,
            MessageType = "text",
            Direction = "outbound",
            CreatedAt = DateTime.UtcNow
        });

        await _messengerRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "SendMessage",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<SendMessageResponse>.Ok(new SendMessageResponse
        {
            RecipientId = recipientId,
            MessageId = messageId
        });
    }

    public async Task<Result<List<ConversationResponse>>> GetConversationsAsync(GetConversationsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<ConversationResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,participants,message_count,updated_time,unread_count",
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.PageId}/conversations", queryParams);
        if (response == null) return Result<List<ConversationResponse>>.Fail("No response from Facebook API.");

        var conversations = JsonConvert.DeserializeObject<List<ConversationResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<ConversationResponse>();

        return Result<List<ConversationResponse>>.Ok(conversations);
    }

    public async Task<Result<List<MessageResponse>>> GetMessagesAsync(GetMessagesRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<MessageResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,message,from,to,created_time",
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.ConversationId}/messages", queryParams);
        if (response == null) return Result<List<MessageResponse>>.Fail("No response from Facebook API.");

        var messages = JsonConvert.DeserializeObject<List<MessageResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<MessageResponse>();

        return Result<List<MessageResponse>>.Ok(messages);
    }

    public async Task<Result<MessengerUserProfile>> GetUserProfileAsync(GetUserProfileRequest request, string pageAccessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<MessengerUserProfile>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,first_name,last_name,profile_pic,locale,timezone,gender",
            ["access_token"] = pageAccessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{request.Psid}", queryParams);
        if (response == null) return Result<MessengerUserProfile>.Fail("No response from Facebook API.");

        var profile = JsonConvert.DeserializeObject<MessengerUserProfile>(JsonConvert.SerializeObject(response)) ?? new MessengerUserProfile();
        return Result<MessengerUserProfile>.Ok(profile);
    }

    public async Task<Result<bool>> MarkSeenAsync(MarkSeenRequest request, string pageAccessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new
        {
            recipient = new { id = request.RecipientId },
            sender_action = "mark_seen"
        };

        var response = await _apiClient.PostAsync<dynamic>($"/me/messages", body, pageAccessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> SendTypingIndicatorAsync(TypingOnRequest request, string pageAccessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new
        {
            recipient = new { id = request.RecipientId },
            sender_action = "typing_on"
        };

        var response = await _apiClient.PostAsync<dynamic>($"/me/messages", body, pageAccessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> SubscribeAppToPageAsync(SubscribeAppRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new { subscribed_fields = request.SubscribedFields };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.PageId}/subscribed_apps", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(response?.success == true);
    }

    public async Task<Result<object>> GetPageSubscriptionsAsync(string pageId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<object>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string> { ["access_token"] = accessToken };
        var response = await _apiClient.GetAsync<object>($"/{pageId}/subscribed_apps", queryParams);
        if (response == null) return Result<object>.Fail("No response from Facebook API.");

        return Result<object>.Ok(response);
    }

    public async Task<Result<bool>> SetMessengerProfileAsync(string pageId, object profileSettings, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var response = await _apiClient.PostAsync<dynamic>($"/me/messenger_profile", profileSettings, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(response?.result?.ToString()?.Contains("success") == true);
    }
}
