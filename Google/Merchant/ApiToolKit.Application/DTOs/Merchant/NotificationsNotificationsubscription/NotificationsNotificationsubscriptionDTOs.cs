using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.NotificationsNotificationsubscription;

/// <summary>Updates an existing notification subscription for a merchant.</summary>
public class UpdateNotificationsubscriptionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>NotificationsubscriptionId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string NotificationsubscriptionId { get; set; } = string.Empty;
    /// <summary>List of fields being updated.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>NotificationSubscription JSON object as documented at https://developers.google.com/merchant/api/reference/rest/notifications_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Creates a notification subscription for a business.</summary>
public class CreateNotificationsubscriptionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>NotificationSubscription JSON object as documented at https://developers.google.com/merchant/api/reference/rest/notifications_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Deletes a notification subscription for a merchant.</summary>
public class DeleteNotificationsubscriptionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>NotificationsubscriptionId (Required.)</summary>
    [Required] public string NotificationsubscriptionId { get; set; } = string.Empty;
}

/// <summary>Gets notification subscriptions for an account.</summary>
public class GetNotificationsubscriptionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>NotificationsubscriptionId (Required.)</summary>
    [Required] public string NotificationsubscriptionId { get; set; } = string.Empty;
}

/// <summary>Gets all the notification subscriptions for a merchant.</summary>
public class ListNotificationsubscriptionsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? PageToken { get; set; }
    /// <summary>Optional.</summary>
    public int? PageSize { get; set; }
}
