using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.UserRolePermission;

/// <summary>Gets one user role permission by ID.</summary>
public class GetUserRolePermissionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>UserRolePermissionId (User profile ID associated with this request.)</summary>
    [Required] public string UserRolePermissionId { get; set; } = string.Empty;
}

/// <summary>Gets a list of user role permissions, possibly filtered.</summary>
public class ListUserRolePermissionsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Select only user role permissions with these IDs.</summary>
    public List<string>? Ids { get; set; }
}
