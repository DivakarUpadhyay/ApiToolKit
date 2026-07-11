using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.YouTube.Subscription;

public class ListSubscriptionsRequest
{
    public bool Mine { get; set; } = true;
    public string? ChannelId { get; set; }
    public int PageSize { get; set; } = 50;
    public string? PageToken { get; set; }
    public string Order { get; set; } = "alphabetical";
}

public class SubscriptionResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("channel_id")]
    public string ChannelId { get; set; } = string.Empty;

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("published_at")]
    public DateTime PublishedAt { get; set; }

    [JsonProperty("thumbnail_url")]
    public string ThumbnailUrl { get; set; } = string.Empty;

    [JsonProperty("new_item_count")]
    public int NewItemCount { get; set; }

    [JsonProperty("activity_type")]
    public string ActivityType { get; set; } = string.Empty;
}

public class SubscriptionListResponse
{
    [JsonProperty("subscriptions")]
    public List<SubscriptionResponse> Subscriptions { get; set; } = new();

    [JsonProperty("next_page_token")]
    public string? NextPageToken { get; set; }

    [JsonProperty("total_results")]
    public int TotalResults { get; set; }
}

public class SubscribeRequest
{
    [Required] public string ChannelId { get; set; } = string.Empty;
}

public class DeleteSubscriptionRequest
{
    [Required] public string SubscriptionId { get; set; } = string.Empty;
}
