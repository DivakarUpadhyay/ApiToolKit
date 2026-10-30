using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.UserMe;

/// <summary>Updates the user that is represented by the caller from pending to verified.</summary>
public class VerifySelfUserRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>VerifySelfRequest JSON object as documented at https://developers.google.com/merchant/api/reference/rest/accounts_v1</summary>
    public object? Body { get; set; }
}
