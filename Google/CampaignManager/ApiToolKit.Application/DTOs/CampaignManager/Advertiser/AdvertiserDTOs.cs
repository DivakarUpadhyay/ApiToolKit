using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.Advertiser;

/// <summary>Inserts a new advertiser.</summary>
public class InsertAdvertiserRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Advertiser JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Gets one advertiser by ID.</summary>
public class GetAdvertiserRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Advertiser ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Advertiser ID.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
}

/// <summary>Updates an existing advertiser.</summary>
public class UpdateAdvertiserRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>Advertiser JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Updates an existing advertiser.</summary>
public class UpdateAdvertiserAltRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Advertiser JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieves a list of advertisers, possibly filtered.</summary>
public class ListAdvertisersRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Select only advertisers with these subaccount IDs.</summary>
    public string? SubaccountId { get; set; }
    /// <summary>Select only advertisers which do not belong to any advertiser group.</summary>
    public bool? IncludeAdvertisersWithoutGroupsOnly { get; set; }
    /// <summary>Allows searching for objects by name or ID.</summary>
    public string? SearchString { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Select only advertisers with these floodlight configuration IDs.</summary>
    public List<string>? FloodlightConfigurationIds { get; set; }
    /// <summary>Select only advertisers which use another advertiser's floodlight configuration.</summary>
    public bool? OnlyParent { get; set; }
    /// <summary>Select only advertisers with the specified status.</summary>
    public string? Status { get; set; }
    /// <summary>Select only advertisers with these advertiser group IDs.</summary>
    public List<string>? AdvertiserGroupIds { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Select only advertisers with these IDs.</summary>
    public List<string>? Ids { get; set; }
}
