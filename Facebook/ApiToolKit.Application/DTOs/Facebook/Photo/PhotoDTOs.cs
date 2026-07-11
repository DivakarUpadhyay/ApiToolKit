using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Photo;

public class UploadPhotoRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public string? Url { get; set; }
    public string? Caption { get; set; }
    public bool Published { get; set; } = true;
    public string? AlbumId { get; set; }
}

public class UploadPhotoResponse
{
    public string Id { get; set; } = string.Empty;
    public string? PostId { get; set; }
}

public class PhotoImageItem
{
    public string Source { get; set; } = string.Empty;
    public int Width { get; set; }
    public int Height { get; set; }
}

public class PhotoResponse
{
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Source { get; set; }
    public string? Link { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string? CreatedTime { get; set; }
    public List<PhotoImageItem> Images { get; set; } = new();
}

public class GetPhotosRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public string? AlbumId { get; set; }
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
    public string Fields { get; set; } = "id,name,source,link,width,height,created_time";
}

public class PhotoPaging
{
    public string? Before { get; set; }
    public string? After { get; set; }
}

public class GetPhotosResponse
{
    public List<PhotoResponse> Data { get; set; } = new();
    public PhotoPaging? Paging { get; set; }
}

public class CreateAlbumRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    public string? Message { get; set; }
    public string? Location { get; set; }
    public string? Privacy { get; set; }
}

public class CreateAlbumResponse
{
    public string Id { get; set; } = string.Empty;
}
