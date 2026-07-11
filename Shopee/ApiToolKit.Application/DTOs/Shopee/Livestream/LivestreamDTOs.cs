using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopee.Livestream;

public class CreateLivestreamRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required] public long ScheduledStartTime { get; set; }
    public string CoverImage { get; set; } = string.Empty;
}

public class CreateLivestreamResponse
{
    public long LivestreamId { get; set; }
    public string RtmpUrl { get; set; } = string.Empty;
    public string StreamKey { get; set; } = string.Empty;
}

public class GetLivestreamDetailRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long LivestreamId { get; set; }
}

public class GetLivestreamDetailResponse
{
    public long LivestreamId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public long ViewerCount { get; set; }
    public long LikeCount { get; set; }
    public long ShareCount { get; set; }
    public string CoverUrl { get; set; } = string.Empty;
}

public class UpdateLivestreamRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long LivestreamId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? CoverImage { get; set; }
}

public class UpdateLivestreamResponse
{
    public long LivestreamId { get; set; }
}

public class EndLivestreamRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long LivestreamId { get; set; }
}

public class EndLivestreamResponse
{
    public long LivestreamId { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class GetLivestreamListRequest
{
    [Required] public long ShopId { get; set; }
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string Status { get; set; } = string.Empty;
}

public class GetLivestreamListResponse
{
    public List<LivestreamBrief> LivestreamList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class LivestreamBrief
{
    public long LivestreamId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public long StartTime { get; set; }
    public long ViewerCount { get; set; }
}

public class AddLivestreamProductRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long LivestreamId { get; set; }
    [Required] public List<long> ItemIdList { get; set; } = new();
}

public class AddLivestreamProductResponse
{
    public long LivestreamId { get; set; }
    public List<long> FailedList { get; set; } = new();
}

public class DeleteLivestreamProductRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long LivestreamId { get; set; }
    [Required] public List<long> ItemIdList { get; set; } = new();
}

public class DeleteLivestreamProductResponse
{
    public long LivestreamId { get; set; }
    public List<long> FailedList { get; set; } = new();
}

public class GetLivestreamProductListRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long LivestreamId { get; set; }
}

public class GetLivestreamProductListResponse
{
    public long LivestreamId { get; set; }
    public List<LivestreamProductInfo> ItemList { get; set; } = new();
}

public class LivestreamProductInfo
{
    public long ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public long Price { get; set; }
    public int Stock { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class PinLivestreamProductRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long LivestreamId { get; set; }
    [Required] public long ItemId { get; set; }
}

public class PinLivestreamProductResponse
{
    public long LivestreamId { get; set; }
    public long ItemId { get; set; }
}

public class GetLivestreamMetricsRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long LivestreamId { get; set; }
}

public class GetLivestreamMetricsResponse
{
    public long LivestreamId { get; set; }
    public long TotalViewer { get; set; }
    public long UniqueViewer { get; set; }
    public long TotalLike { get; set; }
    public long TotalShare { get; set; }
    public long TotalOrder { get; set; }
    public long TotalGmv { get; set; }
    public long PeakViewer { get; set; }
    public int AvgWatchTime { get; set; }
}

public class GetLivestreamCommentRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long LivestreamId { get; set; }
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class GetLivestreamCommentResponse
{
    public List<CommentInfo> CommentList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class CommentInfo
{
    public long CommentId { get; set; }
    public long UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public long CommentTime { get; set; }
}

public class GetLivestreamReplayListRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long LivestreamId { get; set; }
}

public class GetLivestreamReplayListResponse
{
    public List<ReplayInfo> ReplayList { get; set; } = new();
}

public class ReplayInfo
{
    public string ReplayId { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public int Duration { get; set; }
    public long CreateTime { get; set; }
}

public class GetLivestreamCoverRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long LivestreamId { get; set; }
}

public class GetLivestreamCoverResponse
{
    public long LivestreamId { get; set; }
    public string CoverUrl { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
}

public class SendLivestreamNotificationRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long LivestreamId { get; set; }
    [Required] public string NotificationType { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class SendLivestreamNotificationResponse
{
    public bool Success { get; set; }
    public int FailedCount { get; set; }
}

public class GetLivestreamReportRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string StartDate { get; set; } = string.Empty;
    [Required] public string EndDate { get; set; } = string.Empty;
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class GetLivestreamReportResponse
{
    public List<LivestreamReportItem> ReportList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class LivestreamReportItem
{
    public long LivestreamId { get; set; }
    public string Title { get; set; } = string.Empty;
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public long ViewerCount { get; set; }
    public long OrderCount { get; set; }
    public long Gmv { get; set; }
}
