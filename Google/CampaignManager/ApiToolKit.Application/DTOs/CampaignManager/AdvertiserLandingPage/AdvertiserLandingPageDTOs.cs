using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.AdvertiserLandingPage;

/// <summary>Gets one landing page by ID.</summary>
public class GetAdvertiserLandingPageRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Landing page ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>AdvertiserLandingPageId (Landing page ID.)</summary>
    [Required] public string AdvertiserLandingPageId { get; set; } = string.Empty;
}

/// <summary>Updates an existing landing page.</summary>
public class UpdateAdvertiserLandingPageRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>LandingPage JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Inserts a new landing page.</summary>
public class InsertAdvertiserLandingPageRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>LandingPage JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Updates an existing landing page.</summary>
public class UpdateAdvertiserLandingPageAltRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>LandingPage JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieves a list of landing pages.</summary>
public class ListAdvertiserLandingPagesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Allows searching for landing pages by name or ID.</summary>
    public string? SearchString { get; set; }
    /// <summary>Select only landing pages that are associated with these campaigns.</summary>
    public List<string>? CampaignIds { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Select only landing pages that belong to this subaccount.</summary>
    public string? SubaccountId { get; set; }
    /// <summary>Select only archived landing pages.</summary>
    public bool? Archived { get; set; }
    /// <summary>Select only landing pages with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Select only landing pages that belong to these advertisers.</summary>
    public List<string>? AdvertiserIds { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
}
