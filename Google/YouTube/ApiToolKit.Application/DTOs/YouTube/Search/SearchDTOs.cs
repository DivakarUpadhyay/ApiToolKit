using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.YouTube.Search;

public class SearchRequest
{
    [Required] public string Q { get; set; } = string.Empty;
    public string Type { get; set; } = "video";
    public int PageSize { get; set; } = 25;
    public string? PageToken { get; set; }
    public string Order { get; set; } = "relevance";
    public DateTime? PublishedAfter { get; set; }
    public string? ChannelId { get; set; }
    public string? VideoDefinition { get; set; }
    public string? VideoDuration { get; set; }
    public string? VideoType { get; set; }
}

public class SearchResultId
{
    [JsonProperty("kind")]
    public string Kind { get; set; } = string.Empty;

    [JsonProperty("videoId")]
    public string VideoId { get; set; } = string.Empty;

    [JsonProperty("channelId")]
    public string ChannelId { get; set; } = string.Empty;

    [JsonProperty("playlistId")]
    public string PlaylistId { get; set; } = string.Empty;
}

public class SearchResultSnippet
{
    [JsonProperty("publishedAt")]
    public DateTime PublishedAt { get; set; }

    [JsonProperty("channelId")]
    public string ChannelId { get; set; } = string.Empty;

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("channelTitle")]
    public string ChannelTitle { get; set; } = string.Empty;

    [JsonProperty("thumbnail_url")]
    public string ThumbnailUrl { get; set; } = string.Empty;
}

public class SearchResult
{
    [JsonProperty("id")]
    public SearchResultId Id { get; set; } = new();

    [JsonProperty("snippet")]
    public SearchResultSnippet Snippet { get; set; } = new();
}

public class SearchResponse
{
    [JsonProperty("items")]
    public List<SearchResult> Items { get; set; } = new();

    [JsonProperty("next_page_token")]
    public string? NextPageToken { get; set; }

    [JsonProperty("total_results")]
    public int TotalResults { get; set; }

    [JsonProperty("results_per_page")]
    public int ResultsPerPage { get; set; }
}
