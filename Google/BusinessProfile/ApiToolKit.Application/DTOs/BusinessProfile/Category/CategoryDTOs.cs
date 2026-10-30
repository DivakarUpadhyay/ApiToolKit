using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.BusinessProfile.Category;

/// <summary>List business categories for a region and language.</summary>
public class ListCategoriesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>ISO country code, e.g. TW</summary>
    [Required] public string RegionCode { get; set; } = string.Empty;
    /// <summary>e.g. en</summary>
    [Required] public string LanguageCode { get; set; } = string.Empty;
    /// <summary>e.g. displayName=coffee</summary>
    public string? Filter { get; set; }
    /// <summary>Maximum results per page</summary>
    public int? PageSize { get; set; }
    /// <summary>Token for the next page</summary>
    public string? PageToken { get; set; }
}

/// <summary>Get several categories by name.</summary>
public class BatchGetCategoriesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Category names, e.g. categories/gcid:coffee_shop</summary>
    [Required] public List<string> Names { get; set; } = new();
    /// <summary>e.g. en</summary>
    [Required] public string LanguageCode { get; set; } = string.Empty;
    /// <summary>ISO country code</summary>
    public string? RegionCode { get; set; }
}

/// <summary>List attributes available for a category or location.</summary>
public class ListAttributeMetadataRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>categories/gcid:...</summary>
    public string? CategoryName { get; set; }
    /// <summary>locations/{id}</summary>
    public string? Parent { get; set; }
    /// <summary>ISO country code</summary>
    public string? RegionCode { get; set; }
    /// <summary>e.g. en</summary>
    public string? LanguageCode { get; set; }
    /// <summary>Maximum results per page</summary>
    public int? PageSize { get; set; }
    /// <summary>Token for the next page</summary>
    public string? PageToken { get; set; }
}

/// <summary>Get a business chain (brand).</summary>
public class GetChainRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Chain id</summary>
    [Required] public string ChainId { get; set; } = string.Empty;
}

/// <summary>Search business chains by name.</summary>
public class SearchChainsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Chain name</summary>
    [Required] public string ChainName { get; set; } = string.Empty;
    /// <summary>Max results</summary>
    public int? PageSize { get; set; }
}
