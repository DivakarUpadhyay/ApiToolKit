using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.FloodlightActivity;

/// <summary>Updates an existing floodlight activity.</summary>
public class UpdateFloodlightActivityRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>FloodlightActivity JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Updates an existing floodlight activity.</summary>
public class UpdateFloodlightActivityAltRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>FloodlightActivity JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Deletes an existing floodlight activity.</summary>
public class DeleteFloodlightActivityRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Floodlight activity ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>FloodlightActivityId (Floodlight activity ID.)</summary>
    [Required] public string FloodlightActivityId { get; set; } = string.Empty;
}

/// <summary>Retrieves a list of floodlight activities, possibly filtered.</summary>
public class ListFloodlightActivitiesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Select only floodlight activities with the specified IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Select only floodlight activities with the specified tag string.</summary>
    public string? TagString { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Allows searching for objects by name or ID.</summary>
    public string? SearchString { get; set; }
    /// <summary>Select only floodlight activities with the specified floodlight activity group IDs.</summary>
    public List<string>? FloodlightActivityGroupIds { get; set; }
    /// <summary>Select only floodlight activities with the specified floodlight activity group tag string.</summary>
    public string? FloodlightActivityGroupTagString { get; set; }
    /// <summary>Select only floodlight activities with the specified floodlight activity group name.</summary>
    public string? FloodlightActivityGroupName { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Select only floodlight activities for the specified floodlight configuration ID.</summary>
    public string? FloodlightConfigurationId { get; set; }
    /// <summary>Select only floodlight activities for the specified advertiser ID.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>Select only floodlight activities with the specified floodlight activity group type.</summary>
    public string? FloodlightActivityGroupType { get; set; }
}

/// <summary>Gets one floodlight activity by ID.</summary>
public class GetFloodlightActivityRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>FloodlightActivityId (User profile ID associated with this request.)</summary>
    [Required] public string FloodlightActivityId { get; set; } = string.Empty;
}

/// <summary>Generates a tag for a floodlight activity.</summary>
public class GeneratetagFloodlightActivityRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Floodlight activity ID for which we want to generate a tag.</summary>
    public string? FloodlightActivityId { get; set; }
}

/// <summary>Inserts a new floodlight activity.</summary>
public class InsertFloodlightActivityRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>FloodlightActivity JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}
