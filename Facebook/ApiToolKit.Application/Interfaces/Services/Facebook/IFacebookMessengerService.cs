using ApiToolKit.Application.DTOs.Facebook.Messenger;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookMessengerService
{
    Task<Result<SendMessageResponse>> SendMessageAsync(SendMessageRequest request, string pageAccessToken);
    Task<Result<List<ConversationResponse>>> GetConversationsAsync(GetConversationsRequest request, string accessToken);
    Task<Result<List<MessageResponse>>> GetMessagesAsync(GetMessagesRequest request, string accessToken);
    Task<Result<MessengerUserProfile>> GetUserProfileAsync(GetUserProfileRequest request, string pageAccessToken);
    Task<Result<bool>> MarkSeenAsync(MarkSeenRequest request, string pageAccessToken);
    Task<Result<bool>> SendTypingIndicatorAsync(TypingOnRequest request, string pageAccessToken);
    Task<Result<bool>> SubscribeAppToPageAsync(SubscribeAppRequest request, string accessToken);
    Task<Result<object>> GetPageSubscriptionsAsync(string pageId, string accessToken);
    Task<Result<bool>> SetMessengerProfileAsync(string pageId, object profileSettings, string accessToken);
}
