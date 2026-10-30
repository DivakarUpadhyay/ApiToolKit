using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.SearchConsole.Site;

/// <summary>List all Search Console properties the account can access.</summary>
public class ListSitesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
}

/// <summary>Get permission level for one property.</summary>
public class GetSiteRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Property URL, e.g. https://www.example.com/ or sc-domain:example.com</summary>
    [Required] public string SiteUrl { get; set; } = string.Empty;
}

/// <summary>Add a property to Search Console (ownership must then be verified).</summary>
public class AddSiteRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Property URL, e.g. https://www.example.com/ or sc-domain:example.com</summary>
    [Required] public string SiteUrl { get; set; } = string.Empty;
}

/// <summary>Remove a property from Search Console.</summary>
public class DeleteSiteRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Property URL, e.g. https://www.example.com/ or sc-domain:example.com</summary>
    [Required] public string SiteUrl { get; set; } = string.Empty;
}
