using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.Facebook.Threads;

public record ThreadsProfile(
    [property: JsonProperty("id")] string? Id,
    [property: JsonProperty("username")] string? Username,
    [property: JsonProperty("name")] string? Name,
    [property: JsonProperty("threads_profile_picture_url")] string? ThreadsProfilePictureUrl,
    [property: JsonProperty("threads_biography")] string? ThreadsBiography);

public record ThreadsPost(
    [property: JsonProperty("id")] string? Id,
    [property: JsonProperty("text")] string? Text,
    [property: JsonProperty("timestamp")] string? Timestamp,
    [property: JsonProperty("media_type")] string? MediaType,
    [property: JsonProperty("shortcode")] string? Shortcode,
    [property: JsonProperty("media_url")] string? MediaUrl,
    [property: JsonProperty("permalink")] string? Permalink,
    [property: JsonProperty("is_quote_post")] bool? IsQuotePost);

public record ThreadsListResponse(
    [property: JsonProperty("data")] List<ThreadsPost>? Data);

public record CreateTextThreadRequest(
    string Text,
    string? ReplyToId = null);

public record CreateMediaThreadRequest(
    string MediaUrl,
    string MediaType,  // "IMAGE" or "VIDEO"
    string? Text = null,
    string? ReplyToId = null);

public record CreateCarouselThreadRequest(
    List<string> MediaUrls,
    string? Text = null);

public record ThreadContainerResponse(
    [property: JsonProperty("id")] string? Id);

public record PublishThreadRequest(string CreationId);

public record ThreadsPublishResponse(
    [property: JsonProperty("id")] string? Id);

public record ThreadsInsightValue(
    [property: JsonProperty("value")] long? Value,
    [property: JsonProperty("end_time")] string? EndTime);

public record ThreadsInsightMetric(
    [property: JsonProperty("name")] string? Name,
    [property: JsonProperty("period")] string? Period,
    [property: JsonProperty("values")] List<ThreadsInsightValue>? Values,
    [property: JsonProperty("title")] string? Title,
    [property: JsonProperty("id")] string? Id);

public record ThreadsInsightsResponse(
    [property: JsonProperty("data")] List<ThreadsInsightMetric>? Data);

public record ThreadsPublishingLimit(
    [property: JsonProperty("config")] int? Config,
    [property: JsonProperty("quota_usage")] int? QuotaUsage,
    [property: JsonProperty("reply_config")] int? ReplyConfig,
    [property: JsonProperty("reply_quota_usage")] int? ReplyQuotaUsage);

public record ThreadsPublishingLimitResponse(
    [property: JsonProperty("data")] List<ThreadsPublishingLimit>? Data);
