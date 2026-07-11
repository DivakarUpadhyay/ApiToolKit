using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Comment;

public class CreateCommentRequest
{
    [Required] public string ObjectId { get; set; } = string.Empty;
    [Required] public string Message { get; set; } = string.Empty;
    public string? AttachmentUrl { get; set; }
    public string? ParentId { get; set; }
}

public class CreateCommentResponse
{
    public string Id { get; set; } = string.Empty;
}

public class CommentFrom
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class CommentResponse
{
    public string Id { get; set; } = string.Empty;
    public string? Message { get; set; }
    public CommentFrom? From { get; set; }
    public string? CreatedTime { get; set; }
    public bool CanRemove { get; set; }
    public bool CanHide { get; set; }
    public int CommentCount { get; set; }
    public int LikeCount { get; set; }
    public object? Attachment { get; set; }
}

public class GetCommentsRequest
{
    [Required] public string ObjectId { get; set; } = string.Empty;
    public string? Filter { get; set; }
    public bool Summary { get; set; } = true;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
}

public class CommentSummary
{
    public int TotalCount { get; set; }
}

public class CommentPaging
{
    public string? Before { get; set; }
    public string? After { get; set; }
}

public class GetCommentsResponse
{
    public List<CommentResponse> Data { get; set; } = new();
    public CommentPaging? Paging { get; set; }
    public CommentSummary? Summary { get; set; }
}

public class UpdateCommentRequest
{
    [Required] public string CommentId { get; set; } = string.Empty;
    [Required] public string Message { get; set; } = string.Empty;
}

public class HideCommentRequest
{
    [Required] public string CommentId { get; set; } = string.Empty;
    public bool IsHidden { get; set; } = true;
}
