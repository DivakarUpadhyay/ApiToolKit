using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.UserRolePermissionGroup;

/// <summary>Gets a list of all supported user role permission groups.</summary>
public class ListUserRolePermissionGroupsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
}

/// <summary>Gets one user role permission group by ID.</summary>
public class GetUserRolePermissionGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User role permission group ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>UserRolePermissionGroupId (User role permission group ID.)</summary>
    [Required] public string UserRolePermissionGroupId { get; set; } = string.Empty;
}
