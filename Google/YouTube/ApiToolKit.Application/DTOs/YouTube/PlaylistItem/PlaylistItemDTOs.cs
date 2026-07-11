using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.YouTube.PlaylistItem;

public class ListPlaylistItemsRequest
{
    [Required] public string PlaylistId { get; set; } = string.Empty;
    public int PageSize { get; set; } = 50;
    public string? PageToken { get; set; }
}

public class PlaylistItemResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("playlist_id")]
    public string PlaylistId { get; set; } = string.Empty;

    [JsonProperty("video_id")]
    public string VideoId { get; set; } = string.Empty;

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("position")]
    public int Position { get; set; }

    [JsonProperty("published_at")]
    public DateTime PublishedAt { get; set; }

    [JsonProperty("thumbnail_url")]
    public string ThumbnailUrl { get; set; } = string.Empty;

    [JsonProperty("video_owner_channel_title")]
    public string VideoOwnerChannelTitle { get; set; } = string.Empty;

    [JsonProperty("video_owner_channel_id")]
    public string VideoOwnerChannelId { get; set; } = string.Empty;
}

public class PlaylistItemListResponse
{
    [JsonProperty("items")]
    public List<PlaylistItemResponse> Items { get; set; } = new();

    [JsonProperty("next_page_token")]
    public string? NextPageToken { get; set; }

    [JsonProperty("total_results")]
    public int TotalResults { get; set; }
}

public class AddPlaylistItemRequest
{
    [Required] public string PlaylistId { get; set; } = string.Empty;
    [Required] public string VideoId { get; set; } = string.Empty;
    public int? Position { get; set; }
}

public class UpdatePlaylistItemRequest
{
    [Required] public string ItemId { get; set; } = string.Empty;
    public string? PlaylistId { get; set; }
    [Required] public int Position { get; set; }
}

public class DeletePlaylistItemRequest
{
    [Required] public string ItemId { get; set; } = string.Empty;
}
