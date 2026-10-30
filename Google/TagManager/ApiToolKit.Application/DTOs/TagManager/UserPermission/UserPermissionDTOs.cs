using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.TagManager.UserPermission;

/// <summary>List users with access to an account.</summary>
public class ListUserPermissionsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
}

/// <summary>Get one user permission.</summary>
public class GetUserPermissionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>User permission id</summary>
    [Required] public string PermissionId { get; set; } = string.Empty;
}

/// <summary>Grant a user access to an account and containers.</summary>
public class CreateUserPermissionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>User email</summary>
    [Required] public string EmailAddress { get; set; } = string.Empty;
    /// <summary>user or admin</summary>
    [Required] public string AccountPermission { get; set; } = string.Empty;
    /// <summary>Array of { containerId, permission (read, edit, approve, publish) }</summary>
    public object? ContainerAccess { get; set; }
}

/// <summary>Change a user permission.</summary>
public class UpdateUserPermissionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>User permission id</summary>
    [Required] public string PermissionId { get; set; } = string.Empty;
    /// <summary>User email</summary>
    [Required] public string EmailAddress { get; set; } = string.Empty;
    /// <summary>user or admin</summary>
    [Required] public string AccountPermission { get; set; } = string.Empty;
    /// <summary>Array of container access</summary>
    public object? ContainerAccess { get; set; }
}

/// <summary>Remove a user from an account.</summary>
public class DeleteUserPermissionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>User permission id</summary>
    [Required] public string PermissionId { get; set; } = string.Empty;
}
