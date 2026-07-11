using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.YouTube.Playlist;

public class ListPlaylistsRequest
{
    public string? ChannelId { get; set; }
    public int PageSize { get; set; } = 50;
    public string? PageToken { get; set; }
    public bool Mine { get; set; } = false;
}

public class PlaylistResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("channel_id")]
    public string ChannelId { get; set; } = string.Empty;

    [JsonProperty("channel_title")]
    public string ChannelTitle { get; set; } = string.Empty;

    [JsonProperty("published_at")]
    public DateTime PublishedAt { get; set; }

    [JsonProperty("item_count")]
    public int ItemCount { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    [JsonProperty("thumbnail_url")]
    public string ThumbnailUrl { get; set; } = string.Empty;
}

public class PlaylistListResponse
{
    [JsonProperty("playlists")]
    public List<PlaylistResponse> Playlists { get; set; } = new();

    [JsonProperty("next_page_token")]
    public string? NextPageToken { get; set; }

    [JsonProperty("total_results")]
    public int TotalResults { get; set; }
}

public class CreatePlaylistRequest
{
    [Required] public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = "public";
    public string? DefaultLanguage { get; set; }
}

public class UpdatePlaylistRequest
{
    [Required] public string PlaylistId { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public string? DefaultLanguage { get; set; }
}

public class DeletePlaylistRequest
{
    [Required] public string PlaylistId { get; set; } = string.Empty;
}
