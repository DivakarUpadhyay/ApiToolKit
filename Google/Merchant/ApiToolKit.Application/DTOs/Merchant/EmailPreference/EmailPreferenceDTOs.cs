using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.EmailPreference;

/// <summary>Returns the email preferences for a Merchant Center account user.</summary>
public class GetEmailPreferencesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>UserId (Required.)</summary>
    [Required] public string UserId { get; set; } = string.Empty;
}

/// <summary>Updates the email preferences for a Merchant Center account user.</summary>
public class UpdateEmailPreferencesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Identifier.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>UserId (Identifier.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>EmailPreferences JSON object as documented at https://developers.google.com/merchant/api/reference/rest/accounts_v1</summary>
    public object? Body { get; set; }
}
