using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.SearchConsole.Indexing;

/// <summary>Tell Google a job posting or livestream URL was updated or deleted.</summary>
public class PublishUrlNotificationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Page URL</summary>
    [Required] public string Url { get; set; } = string.Empty;
    /// <summary>URL_UPDATED or URL_DELETED</summary>
    [Required] public string Type { get; set; } = string.Empty;
}

/// <summary>Get the latest notification Google received for a URL.</summary>
public class GetUrlNotificationMetadataRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Page URL</summary>
    [Required] public string Url { get; set; } = string.Empty;
}
