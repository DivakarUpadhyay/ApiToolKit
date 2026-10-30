using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.Campaign;

/// <summary>Inserts a new campaign.</summary>
public class InsertCampaignRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Campaign JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieves a list of campaigns, possibly filtered.</summary>
public class ListCampaignsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Select only campaigns that have at least one optimization activity.</summary>
    public bool? AtLeastOneOptimizationActivity { get; set; }
    /// <summary>Select only campaigns whose advertisers belong to these advertiser groups.</summary>
    public List<string>? AdvertiserGroupIds { get; set; }
    /// <summary>Allows searching for campaigns by name or ID.</summary>
    public string? SearchString { get; set; }
    /// <summary>Exclude campaigns with these IDs.</summary>
    public List<string>? ExcludedIds { get; set; }
    /// <summary>Select only archived campaigns.</summary>
    public bool? Archived { get; set; }
    /// <summary>Select only campaigns that belong to this subaccount.</summary>
    public string? SubaccountId { get; set; }
    /// <summary>Select only campaigns with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Select only campaigns that belong to these advertisers.</summary>
    public List<string>? AdvertiserIds { get; set; }
    /// <summary>Select only campaigns that have overridden this event tag ID.</summary>
    public string? OverriddenEventTagId { get; set; }
}

/// <summary>Gets one campaign by ID.</summary>
public class GetCampaignRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Campaign ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CampaignId (Campaign ID.)</summary>
    [Required] public string CampaignId { get; set; } = string.Empty;
}

/// <summary>Updates an existing campaign.</summary>
public class UpdateCampaignRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Campaign JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Updates an existing campaign.</summary>
public class PatchCampaignRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>Campaign JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}
