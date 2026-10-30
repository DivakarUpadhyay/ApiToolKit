using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.Account;

/// <summary>Creates a Merchant Center test account.</summary>
public class CreateTestAccountRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>Account JSON object as documented at https://developers.google.com/merchant/api/reference/rest/accounts_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Creates a Merchant Center account with additional configuration.</summary>
public class CreateAndConfigureAccountRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>CreateAndConfigureAccountRequest JSON object as documented at https://developers.google.com/merchant/api/reference/rest/accounts_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Deletes the specified account regardless of its type: standalone, advanced account or sub-account.</summary>
public class DeleteAccountRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public bool? Force { get; set; }
}

/// <summary>Updates an account regardless of its type: standalone, advanced account or sub-account.</summary>
public class UpdateAccountRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Identifier.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>Account JSON object as documented at https://developers.google.com/merchant/api/reference/rest/accounts_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieves an account from your Merchant Center account.</summary>
public class GetAccountRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
}

/// <summary>Note: For the `accounts.list` method, quota and limits usage are charged for each user, and not for the Merchant Center ID or the advanced account ID.</summary>
public class ListAccountsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? PageToken { get; set; }
    /// <summary>Optional.</summary>
    public int? PageSize { get; set; }
    /// <summary>Optional.</summary>
    public string? Filter { get; set; }
}

/// <summary>List all sub-accounts for a given advanced account.</summary>
public class ListSubaccountsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public int? PageSize { get; set; }
    /// <summary>Optional.</summary>
    public string? PageToken { get; set; }
}
