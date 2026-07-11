using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopee.Media;

public class UploadImageRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public byte[] ImageBytes { get; set; } = Array.Empty<byte>();
    [Required] public string FileName { get; set; } = string.Empty;
}

public class UploadImageByUrlRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string ImageUrl { get; set; } = string.Empty;
}

public class UploadVideoRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string VideoUploadId { get; set; } = string.Empty;
    [Required] public string VideoContent { get; set; } = string.Empty;
    [Required] public int PartSeq { get; set; }
}

public class UploadImageResponse
{
    public string ImageId { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string RequestId { get; set; } = string.Empty;
}

public class UploadVideoResponse
{
    public string VideoUploadId { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
