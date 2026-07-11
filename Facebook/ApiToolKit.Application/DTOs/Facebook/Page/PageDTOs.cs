using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Page;

public class PagePictureData
{
    public string? Url { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsSilhouette { get; set; }
}

public class PagePictureWrapper
{
    public PagePictureData? Data { get; set; }
}

public class PageCoverData
{
    public string? Source { get; set; }
    public string? Id { get; set; }
}

public class PageResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Category { get; set; }
    public string? About { get; set; }
    public string? Description { get; set; }
    [Newtonsoft.Json.JsonProperty("followers_count")] public long Followers { get; set; }
    [Newtonsoft.Json.JsonProperty("fan_count")] public long Likes { get; set; }
    public string? Website { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public PagePictureWrapper? Picture { get; set; }
    public PageCoverData? Cover { get; set; }
    [Newtonsoft.Json.JsonProperty("access_token")] public string? AccessToken { get; set; }
}

public class CreatePagePostRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public string? Message { get; set; }
    public string? Link { get; set; }
    public string? Picture { get; set; }
}

public class CreatePagePostResponse
{
    public string Id { get; set; } = string.Empty;
    public string? PostId { get; set; }
}

public class PageSettingsRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public string? About { get; set; }
    public string? Description { get; set; }
    public string? Website { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}

public class PageInsightsRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public string Metric { get; set; } = string.Empty;
    public string Period { get; set; } = "day";
    public DateTime? Since { get; set; }
    public DateTime? Until { get; set; }
}

public class PagePictureRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public string? Picture { get; set; }
    public string Type { get; set; } = "square";
}

public class CreatePageEventRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Location { get; set; }
}

public class PagingCursor
{
    public string? Before { get; set; }
    public string? After { get; set; }
}

public class GetPagesResponse
{
    public List<PageResponse> Data { get; set; } = new();
    public PagingCursor? Paging { get; set; }
}

// ── Page Tabs ──────────────────────────────────────────────────────────────
public class PageTabResponse
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Link { get; set; }
    public int Position { get; set; }
    [Newtonsoft.Json.JsonProperty("is_non_connection_landing_tab")] public bool IsNonConnectionLandingTab { get; set; }
    [Newtonsoft.Json.JsonProperty("is_permanent")] public bool IsPermanent { get; set; }
}

public class AddPageTabRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    [Required] public string AppId { get; set; } = string.Empty;
}

public class UpdatePageTabRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    [Required] public string TabId { get; set; } = string.Empty;
    public string? CustomName { get; set; }
    public int? Position { get; set; }
    public bool? IsNonConnectionLandingTab { get; set; }
}

// ── Page Milestones ────────────────────────────────────────────────────────
public class PageMilestoneResponse
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    [Newtonsoft.Json.JsonProperty("start_time")] public string? StartTime { get; set; }
    [Newtonsoft.Json.JsonProperty("created_time")] public string? CreatedTime { get; set; }
}

public class CreatePageMilestoneRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    [Required] public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required] public string StartTime { get; set; } = string.Empty;
}

// ── Page Blocked Users ─────────────────────────────────────────────────────
public class PageBlockedUserResponse
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}

public class BlockUserRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    [Required] public string UserId { get; set; } = string.Empty;
}

// ── Page CTA ───────────────────────────────────────────────────────────────
public class PageCtaResponse
{
    public string? Id { get; set; }
    [Newtonsoft.Json.JsonProperty("type")] public string? Type { get; set; }
    [Newtonsoft.Json.JsonProperty("web_url")] public string? WebUrl { get; set; }
}

public class CreatePageCtaRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    [Required] public string Type { get; set; } = string.Empty;
    public string? WebUrl { get; set; }
    public string? AndroidUrl { get; set; }
    public string? IphoneUrl { get; set; }
}

// ── Page Visitor Posts ─────────────────────────────────────────────────────
public class PageVisitorPostsRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
    public string Fields { get; set; } = "id,message,story,created_time,from";
}

// ── Page Labels ────────────────────────────────────────────────────────────
public class PageLabelResponse
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    [Newtonsoft.Json.JsonProperty("creation_time")] public string? CreationTime { get; set; }
}

public class CreatePageLabelRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
}

public class AssignPageLabelRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    [Required] public string LabelId { get; set; } = string.Empty;
    [Required] public string UserId { get; set; } = string.Empty;
}

// ── Published / Scheduled Posts ────────────────────────────────────────────
public class PagePublishedPostsRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
    public string? Since { get; set; }
    public string? Until { get; set; }
    public string Fields { get; set; } = "id,message,story,created_time,permalink_url,full_picture";
}

public class PageScheduledPostsRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
    public string Fields { get; set; } = "id,message,scheduled_publish_time,permalink_url";
}

// ── Page Promotable Posts ──────────────────────────────────────────────────
public class PagePromotablePostsRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public bool IncludeHidden { get; set; } = false;
    public string Fields { get; set; } = "id,message,created_time,permalink_url";
}
