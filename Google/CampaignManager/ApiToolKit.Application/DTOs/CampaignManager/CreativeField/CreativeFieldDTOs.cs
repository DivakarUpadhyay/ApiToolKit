using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.CreativeField;

/// <summary>Updates an existing creative field.</summary>
public class UpdateCreativeFieldRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CreativeField ID.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>CreativeField JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Gets one creative field by ID.</summary>
public class GetCreativeFieldRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Creative Field ID)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CreativeFieldId (Creative Field ID)</summary>
    [Required] public string CreativeFieldId { get; set; } = string.Empty;
}

/// <summary>Inserts a new creative field.</summary>
public class InsertCreativeFieldRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CreativeField JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Updates an existing creative field.</summary>
public class UpdateCreativeFieldAltRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CreativeField JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Deletes an existing creative field.</summary>
public class DeleteCreativeFieldRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CreativeFieldId (User profile ID associated with this request.)</summary>
    [Required] public string CreativeFieldId { get; set; } = string.Empty;
}

/// <summary>Retrieves a list of creative fields, possibly filtered.</summary>
public class ListCreativeFieldsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Select only creative fields with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Allows searching for creative fields by name or ID.</summary>
    public string? SearchString { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Select only creative fields that belong to these advertisers.</summary>
    public List<string>? AdvertiserIds { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
}
