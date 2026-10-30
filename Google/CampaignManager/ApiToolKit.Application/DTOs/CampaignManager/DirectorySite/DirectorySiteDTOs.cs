using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.DirectorySite;

/// <summary>Retrieves a list of directory sites, possibly filtered.</summary>
public class ListDirectorySitesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Allows searching for objects by name, ID or URL.</summary>
    public string? SearchString { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>This search filter is no longer supported and will have no effect on the results returned.</summary>
    public bool? AcceptsInStreamVideoPlacements { get; set; }
    /// <summary>Select only active directory sites.</summary>
    public bool? Active { get; set; }
    /// <summary>Select only directory sites with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Select only directory sites with this Ad Manager network code.</summary>
    public string? DfpNetworkCode { get; set; }
    /// <summary>Select only directory sites that accept publisher paid placements.</summary>
    public bool? AcceptsPublisherPaidPlacements { get; set; }
    /// <summary>This search filter is no longer supported and will have no effect on the results returned.</summary>
    public bool? AcceptsInterstitialPlacements { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
}

/// <summary>Inserts a new directory site.</summary>
public class InsertDirectorySiteRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>DirectorySite JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Gets one directory site by ID.</summary>
public class GetDirectorySiteRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>DirectorySiteId (User profile ID associated with this request.)</summary>
    [Required] public string DirectorySiteId { get; set; } = string.Empty;
}
