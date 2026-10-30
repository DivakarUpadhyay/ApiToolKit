using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.SearchConsole.UrlInspection;

/// <summary>Inspect how Google crawled and indexed a URL.</summary>
public class InspectUrlRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Fully-qualified URL to inspect</summary>
    [Required] public string InspectionUrl { get; set; } = string.Empty;
    /// <summary>Property URL, e.g. https://www.example.com/ or sc-domain:example.com</summary>
    [Required] public string SiteUrl { get; set; } = string.Empty;
    /// <summary>Language for messages, e.g. en-US</summary>
    public string? LanguageCode { get; set; }
}
