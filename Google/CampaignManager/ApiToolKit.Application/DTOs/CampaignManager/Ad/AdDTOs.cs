using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.Ad;

/// <summary>Updates an existing ad.</summary>
public class UpdateAdRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>Ad JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieves a list of ads, possibly filtered.</summary>
public class ListAdsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Select only ads with this advertiser ID.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>Select only ads that require SSL.</summary>
    public bool? SslRequired { get; set; }
    /// <summary>Select only ads with these placement IDs assigned.</summary>
    public List<string>? PlacementIds { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Select only ads with these types.</summary>
    public List<string>? Type { get; set; }
    /// <summary>Select only archived ads.</summary>
    public bool? Archived { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Select only ads that are SSL-compliant.</summary>
    public bool? SslCompliant { get; set; }
    /// <summary>Select only ads with these creative optimization configuration IDs.</summary>
    public List<string>? CreativeOptimizationConfigurationIds { get; set; }
    /// <summary>Select only ads with these landing page IDs.</summary>
    public List<string>? LandingPageIds { get; set; }
    /// <summary>Select only ads with these size IDs.</summary>
    public List<string>? SizeIds { get; set; }
    /// <summary>Select only ads with these audience segment IDs.</summary>
    public List<string>? AudienceSegmentIds { get; set; }
    /// <summary>Select only ads with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Select only active ads.</summary>
    public bool? Active { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Select only ads with this event tag override ID.</summary>
    public string? OverriddenEventTagId { get; set; }
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Select only ads whose list targeting expression use these remarketing list IDs.</summary>
    public List<string>? RemarketingListIds { get; set; }
    /// <summary>Select only ads with these creative IDs assigned.</summary>
    public List<string>? CreativeIds { get; set; }
    /// <summary>Select only dynamic click trackers.</summary>
    public bool? DynamicClickTracker { get; set; }
    /// <summary>Select only ads with these campaign IDs.</summary>
    public List<string>? CampaignIds { get; set; }
    /// <summary>Select default ads with the specified compatibility.</summary>
    public string? Compatibility { get; set; }
    /// <summary>Allows searching for objects by name or ID.</summary>
    public string? SearchString { get; set; }
}

/// <summary>Gets one ad by ID.</summary>
public class GetAdRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Ad ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>AdId (Ad ID.)</summary>
    [Required] public string AdId { get; set; } = string.Empty;
}

/// <summary>Updates an existing ad.</summary>
public class UpdateAdAltRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Ad JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Inserts a new ad.</summary>
public class InsertAdRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Ad JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}
