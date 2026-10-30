using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.UserRole;

/// <summary>Retrieves a list of user roles, possibly filtered.</summary>
public class ListUserRolesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Allows searching for objects by name or ID.</summary>
    public string? SearchString { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Select only account level user roles not associated with any specific subaccount.</summary>
    public bool? AccountUserRoleOnly { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Select only user roles that belong to this subaccount.</summary>
    public string? SubaccountId { get; set; }
    /// <summary>Select only user roles with the specified IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
}

/// <summary>Inserts a new user role.</summary>
public class InsertUserRoleRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>UserRole JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Updates an existing user role.</summary>
public class UpdateUserRoleRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>UserRole JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Gets one user role by ID.</summary>
public class GetUserRoleRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>UserRoleId (User profile ID associated with this request.)</summary>
    [Required] public string UserRoleId { get; set; } = string.Empty;
}

/// <summary>Deletes an existing user role.</summary>
public class DeleteUserRoleRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>UserRoleId (User profile ID associated with this request.)</summary>
    [Required] public string UserRoleId { get; set; } = string.Empty;
}

/// <summary>Updates an existing user role.</summary>
public class UpdateUserRoleAltRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>UserRole JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}
