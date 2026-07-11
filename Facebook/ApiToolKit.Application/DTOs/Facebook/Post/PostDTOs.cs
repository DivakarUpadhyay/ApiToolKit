using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Post;

public class CreatePostRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public string? Message { get; set; }
    public string? Link { get; set; }
    public bool Published { get; set; } = true;
    public long? ScheduledPublishTime { get; set; }
    public object? Targeting { get; set; }
}

public class CreatePostResponse
{
    public string Id { get; set; } = string.Empty;
}

public class PostFrom
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class PostResponse
{
    public string Id { get; set; } = string.Empty;
    public string? Message { get; set; }
    public string? Story { get; set; }
    public string? CreatedTime { get; set; }
    public string? UpdatedTime { get; set; }
    public string? PermalinkUrl { get; set; }
    public string? FullPicture { get; set; }
    public PostFrom? From { get; set; }
}

public class UpdatePostRequest
{
    [Required] public string PostId { get; set; } = string.Empty;
    public string? Message { get; set; }
    public bool? Published { get; set; }
}

public class GetFeedRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
    public string? Before { get; set; }
    public string? Since { get; set; }
    public string? Until { get; set; }
    public string Fields { get; set; } = "id,message,story,created_time,permalink_url,full_picture";
}

public class FeedPaging
{
    public string? Previous { get; set; }
    public string? Next { get; set; }
}

public class FeedResponse
{
    public List<PostResponse> Data { get; set; } = new();
    public FeedPaging? Paging { get; set; }
}
