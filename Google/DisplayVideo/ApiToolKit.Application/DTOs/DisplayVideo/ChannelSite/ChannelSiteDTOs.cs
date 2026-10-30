using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.ChannelSite;

/// <summary>Creates a site in a channel.</summary>
public class CreateSiteRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (The ID of the advertiser that owns the parent channel.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>ChannelId (The ID of the advertiser that owns the parent channel.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ChannelId { get; set; } = string.Empty;
    /// <summary>The ID of the partner that owns the parent channel.</summary>
    public string? PartnerId { get; set; }
    /// <summary>Site JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Lists sites in a channel.</summary>
public class ListSitesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>ChannelId (Required.)</summary>
    [Required] public string ChannelId { get; set; } = string.Empty;
    /// <summary>The ID of the partner that owns the parent channel.</summary>
    public string? PartnerId { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>Allows filtering by site fields.</summary>
    public string? Filter { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
}

/// <summary>Replaces all of the sites under a single channel.</summary>
public class ReplaceSiteRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (The ID of the advertiser that owns the parent channel.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>ChannelId (The ID of the advertiser that owns the parent channel.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ChannelId { get; set; } = string.Empty;
    /// <summary>ReplaceSitesRequest JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Bulk edits sites under a single channel.</summary>
public class BulkEditSiteRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (The ID of the advertiser that owns the parent channel.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>ChannelId (The ID of the advertiser that owns the parent channel.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ChannelId { get; set; } = string.Empty;
    /// <summary>BulkEditSitesRequest JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Deletes a site from a channel.</summary>
public class DeleteSiteRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (The ID of the advertiser that owns the parent channel.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>ChannelId (Required.)</summary>
    [Required] public string ChannelId { get; set; } = string.Empty;
    /// <summary>SiteId (Required.)</summary>
    [Required] public string SiteId { get; set; } = string.Empty;
    /// <summary>The ID of the partner that owns the parent channel.</summary>
    public string? PartnerId { get; set; }
}
