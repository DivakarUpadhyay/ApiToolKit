using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.CreativeFieldValue;

/// <summary>Gets one creative field value by ID.</summary>
public class GetCreativeFieldValueRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CreativeFieldId (User profile ID associated with this request.)</summary>
    [Required] public string CreativeFieldId { get; set; } = string.Empty;
    /// <summary>CreativeFieldValueId (User profile ID associated with this request.)</summary>
    [Required] public string CreativeFieldValueId { get; set; } = string.Empty;
}

/// <summary>Updates an existing creative field value.</summary>
public class UpdateCreativeFieldValueRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CreativeFieldId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string CreativeFieldId { get; set; } = string.Empty;
    /// <summary>CreativeFieldValue ID.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>CreativeFieldValue JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Inserts a new creative field value.</summary>
public class InsertCreativeFieldValueRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CreativeFieldId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string CreativeFieldId { get; set; } = string.Empty;
    /// <summary>CreativeFieldValue JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Updates an existing creative field value.</summary>
public class UpdateCreativeFieldValueAltRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CreativeFieldId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string CreativeFieldId { get; set; } = string.Empty;
    /// <summary>CreativeFieldValue JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieves a list of creative field values, possibly filtered.</summary>
public class ListCreativeFieldValuesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CreativeFieldId (User profile ID associated with this request.)</summary>
    [Required] public string CreativeFieldId { get; set; } = string.Empty;
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Allows searching for creative field values by their values.</summary>
    public string? SearchString { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Select only creative field values with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
}

/// <summary>Deletes an existing creative field value.</summary>
public class DeleteCreativeFieldValueRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Creative field ID for this creative field value.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CreativeFieldId (Creative field ID for this creative field value.)</summary>
    [Required] public string CreativeFieldId { get; set; } = string.Empty;
    /// <summary>CreativeFieldValueId (Creative field ID for this creative field value.)</summary>
    [Required] public string CreativeFieldValueId { get; set; } = string.Empty;
}
