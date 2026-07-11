using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.Firebase.Fcm;

public class FcmNotification
{
    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("body")]
    public string? Body { get; set; }

    [JsonProperty("image")]
    public string? Image { get; set; }
}

public class FcmAndroidNotification
{
    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("body")]
    public string? Body { get; set; }

    [JsonProperty("click_action")]
    public string? ClickAction { get; set; }

    [JsonProperty("color")]
    public string? Color { get; set; }

    [JsonProperty("sound")]
    public string? Sound { get; set; }

    [JsonProperty("tag")]
    public string? Tag { get; set; }

    [JsonProperty("icon")]
    public string? Icon { get; set; }

    [JsonProperty("body_loc_key")]
    public string? BodyLocKey { get; set; }

    [JsonProperty("body_loc_args")]
    public List<string>? BodyLocArgs { get; set; }

    [JsonProperty("title_loc_key")]
    public string? TitleLocKey { get; set; }

    [JsonProperty("title_loc_args")]
    public List<string>? TitleLocArgs { get; set; }

    [JsonProperty("android_small_icon")]
    public string? AndroidSmallIcon { get; set; }

    [JsonProperty("large_icon")]
    public string? LargeIcon { get; set; }

    [JsonProperty("channel_id")]
    public string? ChannelId { get; set; }

    [JsonProperty("notification_priority")]
    public string? NotificationPriority { get; set; }

    [JsonProperty("visibility")]
    public string? Visibility { get; set; }

    [JsonProperty("notification_count")]
    public int? NotificationCount { get; set; }

    [JsonProperty("image_url")]
    public string? ImageUrl { get; set; }
}

public class FcmAndroidConfig
{
    [JsonProperty("priority")]
    public string Priority { get; set; } = "normal";

    [JsonProperty("ttl")]
    public string? Ttl { get; set; }

    [JsonProperty("collapse_key")]
    public string? CollapseKey { get; set; }

    [JsonProperty("restricted_package_name")]
    public string? RestrictedPackageName { get; set; }

    [JsonProperty("data")]
    public Dictionary<string, string>? Data { get; set; }

    [JsonProperty("notification")]
    public FcmAndroidNotification? Notification { get; set; }
}

public class ApsAlert
{
    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("subtitle")]
    public string? Subtitle { get; set; }

    [JsonProperty("body")]
    public string? Body { get; set; }

    [JsonProperty("launch-image")]
    public string? LaunchImage { get; set; }

    [JsonProperty("title-loc-key")]
    public string? TitleLocKey { get; set; }

    [JsonProperty("title-loc-args")]
    public List<string>? TitleLocArgs { get; set; }

    [JsonProperty("loc-key")]
    public string? LocKey { get; set; }

    [JsonProperty("loc-args")]
    public List<string>? LocArgs { get; set; }

    [JsonProperty("action-loc-key")]
    public string? ActionLocKey { get; set; }
}

public class Aps
{
    [JsonProperty("alert")]
    public ApsAlert? Alert { get; set; }

    [JsonProperty("badge")]
    public int? Badge { get; set; }

    [JsonProperty("sound")]
    public string? Sound { get; set; }

    [JsonProperty("content-available")]
    public int? ContentAvailable { get; set; }

    [JsonProperty("mutable-content")]
    public int? MutableContent { get; set; }

    [JsonProperty("category")]
    public string? Category { get; set; }

    [JsonProperty("thread-id")]
    public string? ThreadId { get; set; }
}

public class ApnsPayload
{
    [JsonProperty("aps")]
    public Aps? Aps { get; set; }
}

public class ApnsFcmOptions
{
    [JsonProperty("analytics_label")]
    public string? AnalyticsLabel { get; set; }

    [JsonProperty("image")]
    public string? Image { get; set; }
}

public class FcmApnsConfig
{
    [JsonProperty("headers")]
    public Dictionary<string, string>? Headers { get; set; }

    [JsonProperty("payload")]
    public ApnsPayload? Payload { get; set; }

    [JsonProperty("fcm_options")]
    public ApnsFcmOptions? FcmOptions { get; set; }
}

public class WebpushNotificationAction
{
    [JsonProperty("action")]
    public string? Action { get; set; }

    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("icon")]
    public string? Icon { get; set; }
}

public class WebpushNotification
{
    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("body")]
    public string? Body { get; set; }

    [JsonProperty("icon")]
    public string? Icon { get; set; }

    [JsonProperty("badge")]
    public string? Badge { get; set; }

    [JsonProperty("tag")]
    public string? Tag { get; set; }

    [JsonProperty("image")]
    public string? Image { get; set; }

    [JsonProperty("language")]
    public string? Language { get; set; }

    [JsonProperty("renotify")]
    public bool Renotify { get; set; }

    [JsonProperty("require_interaction")]
    public bool RequireInteraction { get; set; }

    [JsonProperty("silent")]
    public bool Silent { get; set; }

    [JsonProperty("vibrate")]
    public List<int>? Vibrate { get; set; }

    [JsonProperty("data")]
    public object? Data { get; set; }

    [JsonProperty("actions")]
    public List<WebpushNotificationAction>? Actions { get; set; }
}

public class WebpushFcmOptions
{
    [JsonProperty("link")]
    public string? Link { get; set; }

    [JsonProperty("analytics_label")]
    public string? AnalyticsLabel { get; set; }
}

public class FcmWebpushConfig
{
    [JsonProperty("headers")]
    public Dictionary<string, string>? Headers { get; set; }

    [JsonProperty("data")]
    public Dictionary<string, string>? Data { get; set; }

    [JsonProperty("notification")]
    public WebpushNotification? Notification { get; set; }

    [JsonProperty("fcm_options")]
    public WebpushFcmOptions? FcmOptions { get; set; }
}

public class SendToTokenRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string Token { get; set; } = string.Empty;

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Body { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }
    public Dictionary<string, string>? Data { get; set; }
    public FcmAndroidConfig? Android { get; set; }
    public FcmApnsConfig? Apns { get; set; }
    public FcmWebpushConfig? Webpush { get; set; }
}

public class SendToTopicRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string Topic { get; set; } = string.Empty;

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Body { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }
    public Dictionary<string, string>? Data { get; set; }
    public FcmAndroidConfig? Android { get; set; }
    public FcmApnsConfig? Apns { get; set; }
    public FcmWebpushConfig? Webpush { get; set; }
}

public class SendToConditionRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string Condition { get; set; } = string.Empty;

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Body { get; set; } = string.Empty;

    public Dictionary<string, string>? Data { get; set; }
}

public class MulticastRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public List<string> Tokens { get; set; } = new();

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Body { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }
    public Dictionary<string, string>? Data { get; set; }
}

public class FcmMessageResponse
{
    [JsonProperty("name")]
    public string? Name { get; set; }
}

public class MulticastSingleResponse
{
    public string? MessageId { get; set; }
    public MulticastError? Error { get; set; }
}

public class MulticastError
{
    public string? Status { get; set; }
    public string? Message { get; set; }
}

public class MulticastResponse
{
    public int SuccessCount { get; set; }
    public int FailureCount { get; set; }
    public List<MulticastSingleResponse> Responses { get; set; } = new();
}

public class SubscribeToTopicRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public List<string> RegistrationTokens { get; set; } = new();

    [Required]
    public string Topic { get; set; } = string.Empty;
}

public class TopicResult
{
    [JsonProperty("error")]
    public string? Error { get; set; }
}

public class SubscribeToTopicResponse
{
    [JsonProperty("results")]
    public List<TopicResult>? Results { get; set; }

    public int FailureCount { get; set; }
    public int SuccessCount { get; set; }
}

public class UnsubscribeFromTopicRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public List<string> RegistrationTokens { get; set; } = new();

    [Required]
    public string Topic { get; set; } = string.Empty;
}
