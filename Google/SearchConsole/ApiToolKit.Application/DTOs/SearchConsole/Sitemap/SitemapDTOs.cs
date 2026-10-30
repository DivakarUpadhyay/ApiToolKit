using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.SearchConsole.Sitemap;

/// <summary>List sitemaps submitted for a property.</summary>
public class ListSitemapsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Property URL, e.g. https://www.example.com/ or sc-domain:example.com</summary>
    [Required] public string SiteUrl { get; set; } = string.Empty;
    /// <summary>Only list sitemaps inside this sitemap index</summary>
    public string? SitemapIndex { get; set; }
}

/// <summary>Get status, errors and warnings for one sitemap.</summary>
public class GetSitemapRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Property URL, e.g. https://www.example.com/ or sc-domain:example.com</summary>
    [Required] public string SiteUrl { get; set; } = string.Empty;
    /// <summary>Full sitemap URL</summary>
    [Required] public string Feedpath { get; set; } = string.Empty;
}

/// <summary>Submit a sitemap for a property.</summary>
public class SubmitSitemapRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Property URL, e.g. https://www.example.com/ or sc-domain:example.com</summary>
    [Required] public string SiteUrl { get; set; } = string.Empty;
    /// <summary>Full sitemap URL</summary>
    [Required] public string Feedpath { get; set; } = string.Empty;
}

/// <summary>Delete a sitemap from a property.</summary>
public class DeleteSitemapRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Property URL, e.g. https://www.example.com/ or sc-domain:example.com</summary>
    [Required] public string SiteUrl { get; set; } = string.Empty;
    /// <summary>Full sitemap URL</summary>
    [Required] public string Feedpath { get; set; } = string.Empty;
}
