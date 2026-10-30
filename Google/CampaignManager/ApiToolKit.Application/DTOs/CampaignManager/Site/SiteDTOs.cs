using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.Site;

/// <summary>Retrieves a list of sites, possibly filtered.</summary>
public class ListSitesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Select only approved sites.</summary>
    public bool? Approved { get; set; }
    /// <summary>Select only sites with these directory site IDs.</summary>
    public List<string>? DirectorySiteIds { get; set; }
    /// <summary>This search filter is no longer supported and will have no effect on the results returned.</summary>
    public bool? AcceptsInterstitialPlacements { get; set; }
    /// <summary>Select only sites with these campaign IDs.</summary>
    public List<string>? CampaignIds { get; set; }
    /// <summary>Select only sites that have not been mapped to a directory site.</summary>
    public bool? UnmappedSite { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Select only sites with this subaccount ID.</summary>
    public string? SubaccountId { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Select only sites that accept publisher paid placements.</summary>
    public bool? AcceptsPublisherPaidPlacements { get; set; }
    /// <summary>Allows searching for objects by name, ID or keyName.</summary>
    public string? SearchString { get; set; }
    /// <summary>Select only AdWords sites.</summary>
    public bool? AdWordsSite { get; set; }
    /// <summary>Select only sites with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>This search filter is no longer supported and will have no effect on the results returned.</summary>
    public bool? AcceptsInStreamVideoPlacements { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
}

/// <summary>Inserts a new site.</summary>
public class InsertSiteRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Site JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Updates an existing site.</summary>
public class UpdateSiteRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>Site JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Gets one site by ID.</summary>
public class GetSiteRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Site ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>SiteId (Site ID.)</summary>
    [Required] public string SiteId { get; set; } = string.Empty;
}

/// <summary>Updates an existing site.</summary>
public class UpdateSiteAltRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Site JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}
