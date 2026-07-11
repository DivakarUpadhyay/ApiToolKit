using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.YouTube.Comment;

public class ListCommentThreadsRequest
{
    public string? VideoId { get; set; }
    public string? ChannelId { get; set; }
    public int PageSize { get; set; } = 100;
    public string? PageToken { get; set; }
    public string Order { get; set; } = "time";
}

public class CommentResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("text_display")]
    public string TextDisplay { get; set; } = string.Empty;

    [JsonProperty("text_original")]
    public string TextOriginal { get; set; } = string.Empty;

    [JsonProperty("author_display_name")]
    public string AuthorDisplayName { get; set; } = string.Empty;

    [JsonProperty("author_channel_id")]
    public string AuthorChannelId { get; set; } = string.Empty;

    [JsonProperty("like_count")]
    public int LikeCount { get; set; }

    [JsonProperty("published_at")]
    public DateTime PublishedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("parent_id")]
    public string? ParentId { get; set; }
}

public class CommentThreadResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("video_id")]
    public string VideoId { get; set; } = string.Empty;

    [JsonProperty("top_level_comment")]
    public CommentResponse TopLevelComment { get; set; } = new();

    [JsonProperty("total_reply_count")]
    public int TotalReplyCount { get; set; }

    [JsonProperty("can_reply")]
    public bool CanReply { get; set; }
}

public class CommentListResponse
{
    [JsonProperty("comment_threads")]
    public List<CommentThreadResponse> CommentThreads { get; set; } = new();

    [JsonProperty("next_page_token")]
    public string? NextPageToken { get; set; }

    [JsonProperty("total_results")]
    public int TotalResults { get; set; }
}

public class InsertCommentRequest
{
    [Required] public string VideoId { get; set; } = string.Empty;
    [Required] public string TextOriginal { get; set; } = string.Empty;
}

public class ReplyCommentRequest
{
    [Required] public string ParentId { get; set; } = string.Empty;
    [Required] public string TextOriginal { get; set; } = string.Empty;
}

public class UpdateCommentRequest
{
    [Required] public string CommentId { get; set; } = string.Empty;
    [Required] public string TextOriginal { get; set; } = string.Empty;
}

public class DeleteCommentRequest
{
    [Required] public string CommentId { get; set; } = string.Empty;
}

public class ModerationRequest
{
    [Required] public string CommentId { get; set; } = string.Empty;
    [Required] public string ModerationStatus { get; set; } = string.Empty;
}
