using Newtonsoft.Json;
namespace ApiToolKit.Application.DTOs.Facebook.Live;

public record LiveVideo(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("title")] string? Title,
    [property:JsonProperty("description")] string? Description,
    [property:JsonProperty("status")] string? Status,
    [property:JsonProperty("stream_url")] string? StreamUrl,
    [property:JsonProperty("dash_ingest_url")] string? DashIngestUrl,
    [property:JsonProperty("embed_html")] string? EmbedHtml,
    [property:JsonProperty("creation_time")] string? CreationTime,
    [property:JsonProperty("permalink_url")] string? PermalinkUrl,
    [property:JsonProperty("live_views")] long? LiveViews,
    [property:JsonProperty("video")] LiveVideoRef? Video);

public record LiveVideoRef([property:JsonProperty("id")] string? Id);

public record LiveVideosResponse([property:JsonProperty("data")] List<LiveVideo>? Data);

public record CreateLiveVideoRequest(
    string Title,
    string? Description = null,
    string? Status = "LIVE_NOW",
    bool? SaveVod = true,
    bool? Persistent = false);

public record UpdateLiveVideoRequest(
    string? Title = null,
    string? Description = null,
    string? Status = null);

public record LiveVideoError(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("error_type")] string? ErrorType,
    [property:JsonProperty("error_description")] string? ErrorDescription,
    [property:JsonProperty("error_category")] string? ErrorCategory,
    [property:JsonProperty("event_type")] string? EventType,
    [property:JsonProperty("creation_time")] string? CreationTime);

public record LiveVideoErrorsResponse([property:JsonProperty("data")] List<LiveVideoError>? Data);

public record LiveVideoBlockedUser(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("pic_url")] string? PicUrl);

public record LiveVideoBlockedUsersResponse([property:JsonProperty("data")] List<LiveVideoBlockedUser>? Data);
