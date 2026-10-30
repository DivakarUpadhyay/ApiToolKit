using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.AccountPermissionGroup;

/// <summary>Gets one account permission group by ID.</summary>
public class GetAccountPermissionGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>AccountPermissionGroupId (User profile ID associated with this request.)</summary>
    [Required] public string AccountPermissionGroupId { get; set; } = string.Empty;
}

/// <summary>Retrieves the list of account permission groups.</summary>
public class ListAccountPermissionGroupsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
}
