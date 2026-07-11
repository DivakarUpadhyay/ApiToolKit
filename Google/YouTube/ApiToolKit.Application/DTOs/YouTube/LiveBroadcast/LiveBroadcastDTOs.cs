using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.YouTube.LiveBroadcast;

public class ListLiveBroadcastsRequest
{
    public string BroadcastStatus { get; set; } = "active";
    public int PageSize { get; set; } = 50;
    public string? PageToken { get; set; }
}

public class LiveBroadcastResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("scheduled_start_time")]
    public DateTime? ScheduledStartTime { get; set; }

    [JsonProperty("scheduled_end_time")]
    public DateTime? ScheduledEndTime { get; set; }

    [JsonProperty("actual_start_time")]
    public DateTime? ActualStartTime { get; set; }

    [JsonProperty("actual_end_time")]
    public DateTime? ActualEndTime { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    [JsonProperty("stream_id")]
    public string StreamId { get; set; } = string.Empty;

    [JsonProperty("thumbnail_url")]
    public string ThumbnailUrl { get; set; } = string.Empty;

    [JsonProperty("viewer_count")]
    public long ViewerCount { get; set; }
}

public class LiveBroadcastListResponse
{
    [JsonProperty("broadcasts")]
    public List<LiveBroadcastResponse> Broadcasts { get; set; } = new();

    [JsonProperty("next_page_token")]
    public string? NextPageToken { get; set; }

    [JsonProperty("total_results")]
    public int TotalResults { get; set; }
}

public class CreateLiveBroadcastRequest
{
    [Required] public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required] public DateTime ScheduledStartTime { get; set; }
    public DateTime? ScheduledEndTime { get; set; }
    public string PrivacyStatus { get; set; } = "public";
    public bool EnableAutoStart { get; set; } = false;
    public bool EnableAutoStop { get; set; } = false;
}

public class UpdateLiveBroadcastRequest
{
    [Required] public string BroadcastId { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
}

public class TransitionBroadcastRequest
{
    [Required] public string BroadcastId { get; set; } = string.Empty;
    [Required] public string BroadcastStatus { get; set; } = string.Empty;
}

public class BindBroadcastRequest
{
    [Required] public string BroadcastId { get; set; } = string.Empty;
    [Required] public string StreamId { get; set; } = string.Empty;
}
