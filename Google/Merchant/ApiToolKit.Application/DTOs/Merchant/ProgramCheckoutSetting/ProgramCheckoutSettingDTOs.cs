using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.ProgramCheckoutSetting;

/// <summary>Deletes `CheckoutSettings` and unenrolls merchant from `Checkout` program.</summary>
public class DeleteCheckoutSettingsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProgramId (Required.)</summary>
    [Required] public string ProgramId { get; set; } = string.Empty;
}

/// <summary>Creates `CheckoutSettings` for the given merchant.</summary>
public class CreateCheckoutSettingRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProgramId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ProgramId { get; set; } = string.Empty;
    /// <summary>CheckoutSettings JSON object as documented at https://developers.google.com/merchant/api/reference/rest/accounts_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Updates `CheckoutSettings` for the given merchant.</summary>
public class UpdateCheckoutSettingsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Identifier.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProgramId (Identifier.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ProgramId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>CheckoutSettings JSON object as documented at https://developers.google.com/merchant/api/reference/rest/accounts_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Gets `CheckoutSettings` for the given merchant.</summary>
public class GetCheckoutSettingsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProgramId (Required.)</summary>
    [Required] public string ProgramId { get; set; } = string.Empty;
}
