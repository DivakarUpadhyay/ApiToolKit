using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.AccountUserProfile;

/// <summary>Inserts a new account user profile.</summary>
public class InsertAccountUserProfileRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>AccountUserProfile JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieves a list of account user profiles, possibly filtered.</summary>
public class ListAccountUserProfilesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Select only user profiles with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Select only user profiles with the specified user role ID.</summary>
    public string? UserRoleId { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Select only user profiles with the specified subaccount ID.</summary>
    public string? SubaccountId { get; set; }
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Allows searching for objects by name, ID or email.</summary>
    public string? SearchString { get; set; }
    /// <summary>Select only active user profiles.</summary>
    public bool? Active { get; set; }
}

/// <summary>Updates an existing account user profile.</summary>
public class UpdateAccountUserProfileRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>AccountUserProfile JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Updates an existing account user profile.</summary>
public class PatchAccountUserProfileRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>AccountUserProfile JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Gets one account user profile by ID.</summary>
public class GetAccountUserProfileRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>ProfileId (User profile ID associated with this request.)</summary>
    [Required] public string ProfileId { get; set; } = string.Empty;
    /// <summary>AccountUserProfileId (User profile ID associated with this request.)</summary>
    [Required] public string AccountUserProfileId { get; set; } = string.Empty;
}
