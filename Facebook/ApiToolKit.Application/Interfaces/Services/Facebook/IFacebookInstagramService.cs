using ApiToolKit.Application.DTOs.Facebook.Instagram;
using ApiToolKit.Application.DTOs.Facebook.Insight;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookInstagramService
{
    Task<Result<InstagramAccountResponse>> GetInstagramAccountAsync(string igUserId, string accessToken);
    Task<Result<InstagramAccountResponse>> GetLinkedInstagramAccountAsync(string pageId, string accessToken);
    Task<Result<CreateMediaResponse>> CreateMediaAsync(CreateMediaRequest request, string accessToken);
    Task<Result<PublishMediaResponse>> PublishMediaAsync(PublishMediaRequest request, string accessToken);
    Task<Result<List<MediaResponse>>> GetMediaAsync(GetMediaRequest request, string accessToken);
    Task<Result<List<MediaInsightResponse>>> GetMediaInsightsAsync(GetMediaInsightsRequest request, string accessToken);
    Task<Result<HashtagResponse>> GetHashtagAsync(GetHashtagRequest request, string accessToken);
    Task<Result<object>> GetHashtagMediaAsync(GetHashtagMediaRequest request, string accessToken);
    Task<Result<List<IgCommentResponse>>> GetIgCommentsAsync(string mediaId, string accessToken, int limit);
    Task<Result<bool>> ReplyIgCommentAsync(string commentId, string message, string accessToken);
    Task<Result<bool>> DeleteIgCommentAsync(string commentId, string accessToken);
    Task<Result<BusinessDiscoveryResponse>> GetBusinessDiscoveryAsync(BusinessDiscoveryRequest request, string accessToken);
    Task<Result<GetPageInsightsResponse>> GetIgAccountInsightsAsync(string igUserId, List<string> metrics, string period, string accessToken, long? since = null, long? until = null);

    // Stories
    Task<Result<MediaContainerResponse>> CreateStoryAsync(CreateStoryRequest request);

    // Reels
    Task<Result<MediaContainerResponse>> CreateReelAsync(CreateReelRequest request);

    // Carousel
    Task<Result<MediaContainerResponse>> CreateCarouselAsync(CreateCarouselRequest request);

    // Container status
    Task<Result<ContainerStatusResponse>> GetContainerStatusAsync(ContainerStatusRequest request);

    // Publishing limit
    Task<Result<PublishingLimitResponse>> GetPublishingLimitAsync(PublishingLimitRequest request);

    // Mentions
    Task<Result<MentionsResponse>> GetMentionsAsync(GetMentionsRequest request);

    // Tagged media
    Task<Result<TaggedMediaResponse>> GetTaggedMediaAsync(GetTaggedMediaRequest request);

    // Instagram DMs
    Task<Result<MediaContainerResponse>> SendIgDmAsync(SendIgDmRequest request);
    Task<Result<IgConversationsResponse>> GetIgConversationsAsync(GetIgConversationsRequest request);

    // Stories list
    Task<Result<List<MediaResponse>>> GetStoriesAsync(string igUserId, string accessToken, string fields, int limit);

    // Live media
    Task<Result<object>> GetLiveMediaAsync(string igUserId, string accessToken, string fields, int limit);

    // Carousel children
    Task<Result<List<MediaResponse>>> GetMediaChildrenAsync(string mediaId, string accessToken, string fields);

    // Delete media
    Task<Result<bool>> DeleteMediaAsync(string mediaId, string accessToken);

    // Toggle comments on media
    Task<Result<bool>> ToggleCommentsAsync(string mediaId, bool commentsEnabled, string accessToken);

    // Recently searched hashtags
    Task<Result<object>> GetRecentlySearchedHashtagsAsync(string igUserId, string accessToken);

    // Mentioned comment details
    Task<Result<object>> GetMentionedCommentAsync(string igUserId, string commentId, string fields, string accessToken);

    // Mentioned media details
    Task<Result<object>> GetMentionedMediaAsync(string igUserId, string mediaId, string fields, string accessToken);

    // Get single media by ID
    Task<Result<MediaResponse>> GetMediaByIdAsync(string mediaId, string fields, string accessToken);

    // Hide/unhide a comment
    Task<Result<bool>> HideCommentAsync(string commentId, bool hide, string accessToken);

    // Get replies to a comment
    Task<Result<List<IgCommentResponse>>> GetCommentRepliesAsync(string commentId, string accessToken, int limit);

    // Get single comment
    Task<Result<IgCommentResponse>> GetSingleCommentAsync(string commentId, string accessToken);

    // Add top-level comment to media
    Task<Result<AddCommentResponse>> AddCommentAsync(AddCommentRequest request);

    // Business discovery media (other account's published media)
    Task<Result<BusinessDiscoveryMediaResponse>> GetBusinessDiscoveryMediaAsync(BusinessDiscoveryMediaRequest request);

    // Subscribe Instagram webhook
    Task<Result<bool>> SubscribeInstagramWebhookAsync(IgWebhookSubscribeRequest request);

    // Get conversation messages
    Task<Result<ConversationMessagesResponse>> GetConversationMessagesAsync(GetConversationMessagesRequest request);
}
