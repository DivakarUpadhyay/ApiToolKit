using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.YouTube.Video;

public class ListVideosRequest
{
    public string? ChannelId { get; set; }
    public int PageSize { get; set; } = 50;
    public string? PageToken { get; set; }
    public string Order { get; set; } = "date";
    public DateTime? PublishedAfter { get; set; }
    public DateTime? PublishedBefore { get; set; }
}

public class VideoResponse
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

    [JsonProperty("duration")]
    public string Duration { get; set; } = string.Empty;

    [JsonProperty("view_count")]
    public long ViewCount { get; set; }

    [JsonProperty("like_count")]
    public long LikeCount { get; set; }

    [JsonProperty("dislike_count")]
    public long DislikeCount { get; set; }

    [JsonProperty("comment_count")]
    public long CommentCount { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    [JsonProperty("thumbnail_url")]
    public string ThumbnailUrl { get; set; } = string.Empty;

    [JsonProperty("tags")]
    public List<string> Tags { get; set; } = new();

    [JsonProperty("category_id")]
    public string CategoryId { get; set; } = string.Empty;

    [JsonProperty("live_broadcast_content")]
    public string LiveBroadcastContent { get; set; } = string.Empty;
}

public class VideoListResponse
{
    [JsonProperty("videos")]
    public List<VideoResponse> Videos { get; set; } = new();

    [JsonProperty("next_page_token")]
    public string? NextPageToken { get; set; }

    [JsonProperty("total_results")]
    public int TotalResults { get; set; }

    [JsonProperty("results_per_page")]
    public int ResultsPerPage { get; set; }
}

public class GetVideoRequest
{
    [Required] public string VideoId { get; set; } = string.Empty;
}

public class UpdateVideoRequest
{
    [Required] public string VideoId { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<string>? Tags { get; set; }
    public string? CategoryId { get; set; }
    public string? Status { get; set; }
    public string? DefaultLanguage { get; set; }
}

public class DeleteVideoRequest
{
    [Required] public string VideoId { get; set; } = string.Empty;
}

public class RateVideoRequest
{
    [Required] public string VideoId { get; set; } = string.Empty;
    [Required] public string Rating { get; set; } = string.Empty;
}

public class UploadVideoMetadata
{
    [Required] public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<string>? Tags { get; set; }
    public string CategoryId { get; set; } = "22";
    public string PrivacyStatus { get; set; } = "public";
    public string DefaultLanguage { get; set; } = "en";
}

public class UploadVideoResult
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    [JsonProperty("upload_status")]
    public string UploadStatus { get; set; } = string.Empty;

    [JsonProperty("file_name")]
    public string FileName { get; set; } = string.Empty;

    [JsonProperty("error")]
    public string? Error { get; set; }
}

public class BulkUploadResult
{
    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("succeeded")]
    public int Succeeded { get; set; }

    [JsonProperty("failed")]
    public int Failed { get; set; }

    [JsonProperty("results")]
    public List<UploadVideoResult> Results { get; set; } = new();
}
