using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.Subaccount;

/// <summary>Updates an existing subaccount.</summary>
public class UpdateSubaccountRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Subaccount JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Updates an existing subaccount.</summary>
public class PatchSubaccountRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>Subaccount JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Inserts a new subaccount.</summary>
public class InsertSubaccountRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Subaccount JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Gets one subaccount by ID.</summary>
public class GetSubaccountRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Subaccount ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>SubaccountId (Subaccount ID.)</summary>
    [Required] public string SubaccountId { get; set; } = string.Empty;
}

/// <summary>Gets a list of subaccounts, possibly filtered.</summary>
public class ListSubaccountsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Allows searching for objects by name or ID.</summary>
    public string? SearchString { get; set; }
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Select only subaccounts with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
}
