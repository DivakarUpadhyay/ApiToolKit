using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.YouTube.Channel;

public class GetChannelRequest
{
    public string? ChannelId { get; set; }
    public string? ForHandle { get; set; }
}

public class ChannelResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("custom_url")]
    public string CustomUrl { get; set; } = string.Empty;

    [JsonProperty("published_at")]
    public DateTime PublishedAt { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; } = string.Empty;

    [JsonProperty("view_count")]
    public long ViewCount { get; set; }

    [JsonProperty("subscriber_count")]
    public long SubscriberCount { get; set; }

    [JsonProperty("video_count")]
    public long VideoCount { get; set; }

    [JsonProperty("thumbnail_url")]
    public string ThumbnailUrl { get; set; } = string.Empty;

    [JsonProperty("banner_image_url")]
    public string BannerImageUrl { get; set; } = string.Empty;
}

public class ListMyChannelsResponse
{
    [JsonProperty("channels")]
    public List<ChannelResponse> Channels { get; set; } = new();

    [JsonProperty("next_page_token")]
    public string? NextPageToken { get; set; }

    [JsonProperty("total_results")]
    public int TotalResults { get; set; }
}

public class UpdateChannelRequest
{
    [Required] public string ChannelId { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? DefaultLanguage { get; set; }
    public string? Country { get; set; }
    public string? Keywords { get; set; }
}

public class ChannelSectionRequest
{
    [Required] public string ChannelId { get; set; } = string.Empty;
    [Required] public string Title { get; set; } = string.Empty;
    [Required] public string Type { get; set; } = string.Empty;
}
