using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.AdvertiserGroup;

/// <summary>Deletes an existing advertiser group.</summary>
public class DeleteAdvertiserGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>AdvertiserGroupId (User profile ID associated with this request.)</summary>
    [Required] public string AdvertiserGroupId { get; set; } = string.Empty;
}

/// <summary>Retrieves a list of advertiser groups, possibly filtered.</summary>
public class ListAdvertiserGroupsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Allows searching for objects by name or ID.</summary>
    public string? SearchString { get; set; }
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Select only advertiser groups with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
}

/// <summary>Updates an existing advertiser group.</summary>
public class UpdateAdvertiserGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>AdvertiserGroup JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Inserts a new advertiser group.</summary>
public class InsertAdvertiserGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>AdvertiserGroup JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Gets one advertiser group by ID.</summary>
public class GetAdvertiserGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>AdvertiserGroupId (User profile ID associated with this request.)</summary>
    [Required] public string AdvertiserGroupId { get; set; } = string.Empty;
}

/// <summary>Updates an existing advertiser group.</summary>
public class PatchAdvertiserGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>AdvertiserGroup JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}
