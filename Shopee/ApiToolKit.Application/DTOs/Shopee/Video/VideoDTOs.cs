using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopee.Video;

public class UploadVideoRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public byte[] VideoData { get; set; } = Array.Empty<byte>();
    [Required] public string FileName { get; set; } = string.Empty;
}

public class UploadVideoResponse
{
    public string VideoId { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public int Duration { get; set; }
    public int Size { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class GetVideoListRequest
{
    [Required] public long ShopId { get; set; }
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string Status { get; set; } = string.Empty;
}

public class GetVideoListResponse
{
    public List<VideoInfo> VideoList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}

public class VideoInfo
{
    public string VideoId { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public int Duration { get; set; }
    public int Size { get; set; }
    public string Status { get; set; } = string.Empty;
    public long CreateTime { get; set; }
}

public class GetVideoRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string VideoId { get; set; } = string.Empty;
}

public class GetVideoResponse
{
    public string VideoId { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public int Duration { get; set; }
    public int Size { get; set; }
    public string Status { get; set; } = string.Empty;
    public long CreateTime { get; set; }
    public string Title { get; set; } = string.Empty;
}

public class DeleteVideoRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public List<string> VideoIdList { get; set; } = new();
}

public class DeleteVideoResponse
{
    public List<string> FailedList { get; set; } = new();
}

public class UpdateVideoRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string VideoId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}

public class UpdateVideoResponse
{
    public string VideoId { get; set; } = string.Empty;
}

public class GetVideoUploadStatusRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string VideoId { get; set; } = string.Empty;
}

public class GetVideoUploadStatusResponse
{
    public string VideoId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int Progress { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class AddVideoToItemRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long ItemId { get; set; }
    [Required] public string VideoId { get; set; } = string.Empty;
}

public class AddVideoToItemResponse
{
    public long ItemId { get; set; }
    public string VideoId { get; set; } = string.Empty;
}

public class DeleteVideoFromItemRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long ItemId { get; set; }
    [Required] public string VideoId { get; set; } = string.Empty;
}

public class DeleteVideoFromItemResponse
{
    public long ItemId { get; set; }
}

public class GetItemVideoListRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long ItemId { get; set; }
}

public class GetItemVideoListResponse
{
    public List<VideoInfo> VideoList { get; set; } = new();
}
