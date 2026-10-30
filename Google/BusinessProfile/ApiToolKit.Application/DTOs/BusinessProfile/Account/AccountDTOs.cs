using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.BusinessProfile.Account;

/// <summary>List Business Profile accounts for the signed-in user.</summary>
public class ListAccountsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Maximum results per page</summary>
    public int? PageSize { get; set; }
    /// <summary>Token for the next page</summary>
    public string? PageToken { get; set; }
}

/// <summary>Get one Business Profile account.</summary>
public class GetAccountRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
}

/// <summary>Create a location group or organization account.</summary>
public class CreateAccountRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Account name</summary>
    [Required] public string AccountName { get; set; } = string.Empty;
    /// <summary>LOCATION_GROUP or ORGANIZATION</summary>
    [Required] public string Type { get; set; } = string.Empty;
    /// <summary>Owner account resource name, e.g. accounts/123</summary>
    [Required] public string PrimaryOwner { get; set; } = string.Empty;
}

/// <summary>Update an account (fields in UpdateMask).</summary>
public class UpdateAccountRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Comma-separated fields to update</summary>
    [Required] public string UpdateMask { get; set; } = string.Empty;
    /// <summary>Account name</summary>
    public string? AccountName { get; set; }
    /// <summary>Validate the request without applying it</summary>
    public bool? ValidateOnly { get; set; }
}

/// <summary>List admins of an account.</summary>
public class ListAccountAdminsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
}

/// <summary>Invite a user to be an admin of an account.</summary>
public class InviteAccountAdminRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Email address of the user</summary>
    [Required] public string Admin { get; set; } = string.Empty;
    /// <summary>PRIMARY_OWNER, OWNER, MANAGER, SITE_MANAGER</summary>
    [Required] public string Role { get; set; } = string.Empty;
}

/// <summary>Change the role of an account admin.</summary>
public class UpdateAccountAdminRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Admin id</summary>
    [Required] public string AdminId { get; set; } = string.Empty;
    /// <summary>New role</summary>
    [Required] public string Role { get; set; } = string.Empty;
}

/// <summary>Remove an admin from an account.</summary>
public class RemoveAccountAdminRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Admin id</summary>
    [Required] public string AdminId { get; set; } = string.Empty;
}

/// <summary>List pending invitations for an account.</summary>
public class ListInvitationsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>e.g. target_type=ACCOUNTS_ONLY</summary>
    public string? Filter { get; set; }
}

/// <summary>Accept an invitation.</summary>
public class AcceptInvitationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Invitation id</summary>
    [Required] public string InvitationId { get; set; } = string.Empty;
}

/// <summary>Decline an invitation.</summary>
public class DeclineInvitationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Invitation id</summary>
    [Required] public string InvitationId { get; set; } = string.Empty;
}

/// <summary>Get the Pub/Sub notification setting of an account (Notifications API).</summary>
public class GetNotificationSettingRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
}

/// <summary>Set the Pub/Sub topic and notification types for an account.</summary>
public class UpdateNotificationSettingRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>projects/{project}/topics/{topic}</summary>
    [Required] public string PubsubTopic { get; set; } = string.Empty;
    /// <summary>e.g. NEW_REVIEW, UPDATED_REVIEW, GOOGLE_UPDATE, NEW_CUSTOMER_MEDIA, VOICE_OF_MERCHANT_UPDATED</summary>
    [Required] public List<string> NotificationTypes { get; set; } = new();
}
