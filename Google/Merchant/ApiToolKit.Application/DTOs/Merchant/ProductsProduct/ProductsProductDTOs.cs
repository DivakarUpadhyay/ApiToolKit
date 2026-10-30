using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.ProductsProduct;

/// <summary>Lists the processed products in your Merchant Center account.</summary>
public class ListProductsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>A page token, received from a previous `ListProducts` call.</summary>
    public string? PageToken { get; set; }
    /// <summary>The maximum number of products to return.</summary>
    public int? PageSize { get; set; }
}

/// <summary>Retrieves the processed product from your Merchant Center account.</summary>
public class GetProductRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProductId (Required.)</summary>
    [Required] public string ProductId { get; set; } = string.Empty;
}
