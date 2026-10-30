using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.Homepage;

/// <summary>Unclaims a store's homepage.</summary>
public class UnclaimHomepageRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>UnclaimHomepageRequest JSON object as documented at https://developers.google.com/merchant/api/reference/rest/accounts_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Claims a store's homepage.</summary>
public class ClaimHomepageRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>ClaimHomepageRequest JSON object as documented at https://developers.google.com/merchant/api/reference/rest/accounts_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieves a store's homepage.</summary>
public class GetHomepageRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
}

/// <summary>Updates a store's homepage.</summary>
public class UpdateHomepageRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Identifier.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>Homepage JSON object as documented at https://developers.google.com/merchant/api/reference/rest/accounts_v1</summary>
    public object? Body { get; set; }
}
