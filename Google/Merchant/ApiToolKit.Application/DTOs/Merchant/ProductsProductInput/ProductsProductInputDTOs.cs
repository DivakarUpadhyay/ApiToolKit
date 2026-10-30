using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.ProductsProductInput;

/// <summary>Uploads a product input to your Merchant Center account.</summary>
public class InsertProductInputRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? DataSource { get; set; }
    /// <summary>ProductInput JSON object as documented at https://developers.google.com/merchant/api/reference/rest/products_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Deletes a product input from your Merchant Center account.</summary>
public class DeleteProductInputRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProductInputId (Required.)</summary>
    [Required] public string ProductInputId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? DataSource { get; set; }
}

/// <summary>Updates the existing product input in your Merchant Center account.</summary>
public class UpdateProductInputRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Identifier.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProductInputId (Identifier.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ProductInputId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? DataSource { get; set; }
    /// <summary>Optional.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>ProductInput JSON object as documented at https://developers.google.com/merchant/api/reference/rest/products_v1</summary>
    public object? Body { get; set; }
}
