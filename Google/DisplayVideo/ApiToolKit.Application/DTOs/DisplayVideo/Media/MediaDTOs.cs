using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.Media;

/// <summary>Downloads media.</summary>
public class DownloadMediaRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>DownloadId (Name of the media that is being downloaded.)</summary>
    [Required] public string DownloadId { get; set; } = string.Empty;
}

/// <summary>Download the file contents of: Downloads media.</summary>
public class DownloadMediaContentRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>DownloadId (Name of the media that is being downloaded.)</summary>
    [Required] public string DownloadId { get; set; } = string.Empty;
}
