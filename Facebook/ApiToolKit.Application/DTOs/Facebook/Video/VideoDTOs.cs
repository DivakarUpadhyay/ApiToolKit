using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Video;

public class UploadVideoRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public string? FileUrl { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool Published { get; set; } = true;
    public long? ScheduledPublishTime { get; set; }
}

public class UploadVideoResponse
{
    public string Id { get; set; } = string.Empty;
    public string? UploadSessionId { get; set; }
}

public class VideoFormatItem
{
    public string? EmbedHtml { get; set; }
    public string? Filter { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string? Picture { get; set; }
}

public class VideoResponse
{
    public string Id { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Source { get; set; }
    public string? EmbedHtml { get; set; }
    public string? Status { get; set; }
    public double Length { get; set; }
    public string? CreatedTime { get; set; }
    public string? UpdatedTime { get; set; }
    public List<VideoFormatItem> Format { get; set; } = new();
}

public class VideoStatusResponse
{
    public string VideoStatus { get; set; } = string.Empty;
    public int ProcessingProgress { get; set; }
}

public class GetVideosRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
    public string Fields { get; set; } = "id,title,description,source,embed_html,status,length,created_time,updated_time";
}

public class VideoPaging
{
    public string? Before { get; set; }
    public string? After { get; set; }
}

public class GetVideosResponse
{
    public List<VideoResponse> Data { get; set; } = new();
    public VideoPaging? Paging { get; set; }
}
