using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Instagram;

public class InstagramAccountResponse
{
    public string Id { get; set; } = string.Empty;
    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? Biography { get; set; }
    public long FollowersCount { get; set; }
    public long FollowsCount { get; set; }
    public int MediaCount { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Website { get; set; }
    public string? IgId { get; set; }
}

public class UserTag
{
    public string Username { get; set; } = string.Empty;
    public double X { get; set; }
    public double Y { get; set; }
}

public class CreateMediaRequest
{
    [Required] public string IgUserId { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string? Caption { get; set; }
    public string MediaType { get; set; } = "IMAGE";
    public bool IsCarouselItem { get; set; }
    public string? LocationId { get; set; }
    public List<UserTag>? UserTags { get; set; }
    public bool? IsAiGenerated { get; set; }
    public bool? IsPaidPartnership { get; set; }
    public string? AltText { get; set; }
    public List<string>? BrandedContentSponsorIds { get; set; }
}

public class CreateMediaResponse
{
    public string Id { get; set; } = string.Empty;
    public string? CreationId { get; set; }
}

public class PublishMediaRequest
{
    [Required] public string IgUserId { get; set; } = string.Empty;
    [Required] public string CreationId { get; set; } = string.Empty;
}

public class PublishMediaResponse
{
    public string Id { get; set; } = string.Empty;
}

public class GetMediaRequest
{
    [Required] public string IgUserId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
    public string Fields { get; set; } = "id,caption,media_type,media_url,thumbnail_url,permalink,timestamp,like_count,comments_count";
}

public class MediaResponse
{
    public string Id { get; set; } = string.Empty;
    public string? Caption { get; set; }
    public string? MediaType { get; set; }
    public string? MediaUrl { get; set; }
    public string? Thumbnail { get; set; }
    public string? Permalink { get; set; }
    public string? Timestamp { get; set; }
    public int LikeCount { get; set; }
    public int CommentsCount { get; set; }
}

public class GetMediaInsightsRequest
{
    [Required] public string MediaId { get; set; } = string.Empty;
    public List<string> Metrics { get; set; } = new();
}

public class MediaInsightValue
{
    public object? Value { get; set; }
}

public class MediaInsightResponse
{
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Period { get; set; }
    public List<MediaInsightValue> Values { get; set; } = new();
    public string? Title { get; set; }
    public string? Description { get; set; }
}

public class GetHashtagRequest
{
    [Required] public string HashtagName { get; set; } = string.Empty;
    [Required] public string IgUserId { get; set; } = string.Empty;
}

public class HashtagResponse
{
    public string Id { get; set; } = string.Empty;
}

public class GetHashtagMediaRequest
{
    [Required] public string HashtagId { get; set; } = string.Empty;
    [Required] public string IgUserId { get; set; } = string.Empty;
    public string? MediaType { get; set; }
    public List<string> Fields { get; set; } = new();
    public int Limit { get; set; } = 25;
}

public class IgCommentResponse
{
    public string Id { get; set; } = string.Empty;
    public string? Text { get; set; }
    public string? Username { get; set; }
    public string? Timestamp { get; set; }
    public int LikeCount { get; set; }
    public bool Hidden { get; set; }
}

public class BusinessDiscoveryRequest
{
    [Required] public string IgUserId { get; set; } = string.Empty;
    [Required] public string Username { get; set; } = string.Empty;
}

public class BusinessDiscoveryResponse
{
    public string Id { get; set; } = string.Empty;
    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? Biography { get; set; }
    public long FollowersCount { get; set; }
    public int MediaCount { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Website { get; set; }
}

// ---- Shared Container Response ----
public class MediaContainerResponse
{
    public string? Id { get; set; }
}

// ---- Stories DTOs ----
public class CreateStoryRequest
{
    [Required] public string IgUserId { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? VideoUrl { get; set; }
    [Required] public string AccessToken { get; set; } = string.Empty;
}

// ---- Reels DTOs ----
public class CreateReelRequest
{
    [Required] public string IgUserId { get; set; } = string.Empty;
    [Required] public string VideoUrl { get; set; } = string.Empty;
    public string? Caption { get; set; }
    public string? CoverUrl { get; set; }
    public bool ShareToFeed { get; set; } = true;
    [Required] public string AccessToken { get; set; } = string.Empty;
}

// ---- Carousel DTOs ----
public class CarouselItemRequest
{
    public string? ImageUrl { get; set; }
    public string? VideoUrl { get; set; }
    public bool IsCarouselItem { get; set; } = true;
}
public class CreateCarouselRequest
{
    [Required] public string IgUserId { get; set; } = string.Empty;
    [Required] public List<CarouselItemRequest> Items { get; set; } = new();
    public string? Caption { get; set; }
    public string? LocationId { get; set; }
    [Required] public string AccessToken { get; set; } = string.Empty;
}

// ---- Container Status DTOs ----
public class ContainerStatusRequest
{
    [Required] public string ContainerId { get; set; } = string.Empty;
    [Required] public string AccessToken { get; set; } = string.Empty;
}
public class ContainerStatusResponse
{
    public string? StatusCode { get; set; }
    public string? Status { get; set; }
    public string? Id { get; set; }
}

// ---- Publishing Limit DTOs ----
public class PublishingLimitRequest
{
    [Required] public string IgUserId { get; set; } = string.Empty;
    [Required] public string AccessToken { get; set; } = string.Empty;
}
public class PublishingLimitResponse
{
    public int? Config { get; set; }
    public int? Quota100 { get; set; }
    public string? QuotaDuration { get; set; }
}

// ---- Mentions DTOs ----
public class GetMentionsRequest
{
    [Required] public string IgUserId { get; set; } = string.Empty;
    public int Limit { get; set; } = 10;
    [Required] public string AccessToken { get; set; } = string.Empty;
}
public class MentionDto
{
    public string? Id { get; set; }
    public string? Caption { get; set; }
    public string? MediaType { get; set; }
    public string? MediaUrl { get; set; }
    public string? Timestamp { get; set; }
}
public class MentionsResponse
{
    public List<MentionDto>? Data { get; set; }
}

// ---- Tagged Media DTOs ----
public class GetTaggedMediaRequest
{
    [Required] public string IgUserId { get; set; } = string.Empty;
    public int Limit { get; set; } = 10;
    [Required] public string AccessToken { get; set; } = string.Empty;
}
public class TaggedMediaResponse
{
    public List<MentionDto>? Data { get; set; }
}

// ---- Instagram DM DTOs ----
public class SendIgDmRequest
{
    [Required] public string IgUserId { get; set; } = string.Empty;
    [Required] public string RecipientIgId { get; set; } = string.Empty;
    [Required] public string Message { get; set; } = string.Empty;
    [Required] public string AccessToken { get; set; } = string.Empty;
}
public class GetIgConversationsRequest
{
    [Required] public string IgUserId { get; set; } = string.Empty;
    [Required] public string AccessToken { get; set; } = string.Empty;
}
public class IgConversationDto
{
    public string? Id { get; set; }
    public string? UpdatedTime { get; set; }
    public int? MessageCount { get; set; }
}
public class IgConversationsResponse
{
    public List<IgConversationDto>? Data { get; set; }
}

// ---- User Tags DTOs ----
public class GetUserTagsRequest
{
    [Required] public string IgUserId { get; set; } = string.Empty;
    public int Limit { get; set; } = 10;
    [Required] public string AccessToken { get; set; } = string.Empty;
}

// ---- Live Video DTOs ----
public class CreateLiveVideoRequest
{
    [Required] public string IgUserId { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Description { get; set; }
    [Required] public string AccessToken { get; set; } = string.Empty;
}
public class LiveVideoResponse
{
    public string? Id { get; set; }
    public string? StreamUrl { get; set; }
    public string? SecureStreamUrl { get; set; }
    public string? Status { get; set; }
}

// ---- Add Comment DTOs ----
public class AddCommentRequest
{
    [Required] public string MediaId { get; set; } = string.Empty;
    [Required] public string Message { get; set; } = string.Empty;
    [Required] public string AccessToken { get; set; } = string.Empty;
}
public class AddCommentResponse
{
    public string Id { get; set; } = string.Empty;
}

// ---- Business Discovery Media DTOs ----
public class BusinessDiscoveryMediaRequest
{
    [Required] public string IgUserId { get; set; } = string.Empty;
    [Required] public string Username { get; set; } = string.Empty;
    public int Limit { get; set; } = 10;
    [Required] public string AccessToken { get; set; } = string.Empty;
}
public class BusinessDiscoveryMediaResponse
{
    public string? Id { get; set; }
    public string? Username { get; set; }
    public List<MediaResponse>? Media { get; set; }
}

// ---- Instagram Webhook Subscribe ----
public class IgWebhookSubscribeRequest
{
    [Required] public string AppId { get; set; } = string.Empty;
    [Required] public string CallbackUrl { get; set; } = string.Empty;
    [Required] public string VerifyToken { get; set; } = string.Empty;
    public string Fields { get; set; } = "comments,mentions,messages";
    [Required] public string AccessToken { get; set; } = string.Empty;
}

// ---- Conversation Messages DTOs ----
public class GetConversationMessagesRequest
{
    [Required] public string ConversationId { get; set; } = string.Empty;
    public int Limit { get; set; } = 20;
    [Required] public string AccessToken { get; set; } = string.Empty;
}
public class IgMessageDto
{
    public string? Id { get; set; }
    public string? Message { get; set; }
    public string? From { get; set; }
    public string? CreatedTime { get; set; }
}
public class ConversationMessagesResponse
{
    public List<IgMessageDto>? Data { get; set; }
}
