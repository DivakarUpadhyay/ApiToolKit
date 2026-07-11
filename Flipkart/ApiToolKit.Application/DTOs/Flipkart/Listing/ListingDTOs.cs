using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Flipkart.Listing;

// ── Create / Update Listing ──────────────────────────────────────────────────

public class CreateListingRequest
{
    [Required] public Dictionary<string, ListingSkuData> Listings { get; set; } = new();
}

public class UpdateListingRequest
{
    [Required] public Dictionary<string, ListingSkuData> Listings { get; set; } = new();
}

public class ListingSkuData
{
    public string? ProductId { get; set; }
    public ListingPrice? Price { get; set; }
    public ListingTax? Tax { get; set; }
    public string? ListingStatus { get; set; }
    public ListingShippingFees? ShippingFees { get; set; }
    public string? FulfillmentProfile { get; set; }
    public ListingFulfillment? Fulfillment { get; set; }
    public List<ListingPackage>? Packages { get; set; }
    public List<ListingLocation>? Locations { get; set; }
    public ListingAddressLabel? AddressLabel { get; set; }
    public ListingDatingLabel? DatingLabel { get; set; }
    public string? ArchivedStatus { get; set; }
}

public class ListingPrice
{
    public int Mrp { get; set; }
    public int SellingPrice { get; set; }
    public string Currency { get; set; } = "INR";
}

public class ListingTax
{
    public string? Hsn { get; set; }
    public string? TaxCode { get; set; }
}

public class ListingShippingFees
{
    public int Local { get; set; }
    public int Zonal { get; set; }
    public int National { get; set; }
    public string Currency { get; set; } = "INR";
}

public class ListingFulfillment
{
    public int DispatchSla { get; set; }
    public string? ShippingProvider { get; set; }
    public string? ProcurementType { get; set; }
}

public class ListingPackage
{
    public string? Name { get; set; }
    public PackageDimensions? Dimensions { get; set; }
    public decimal Weight { get; set; }
    public string? Description { get; set; }
    public PackageHandling? Handling { get; set; }
}

public class PackageDimensions
{
    public decimal Length { get; set; }
    public decimal Breadth { get; set; }
    public decimal Height { get; set; }
}

public class PackageHandling
{
    public bool Fragile { get; set; }
}

public class ListingLocation
{
    [Required] public string Id { get; set; } = string.Empty;
    public string? Status { get; set; }
    public int Inventory { get; set; }
}

public class ListingAddressLabel
{
    public List<string>? ManufacturerDetails { get; set; }
    public List<string>? ImporterDetails { get; set; }
    public List<string>? PackerDetails { get; set; }
    public List<string>? CountriesOfOrigin { get; set; }
}

public class ListingDatingLabel
{
    public long? MfgDate { get; set; }
    public long? ShelfLife { get; set; }
}

// ── Get Listings ─────────────────────────────────────────────────────────────

public class GetListingsBySkuRequest
{
    [Required] public string SkuIds { get; set; } = string.Empty;
}

public class GetListingDetailsRequest
{
    [Required] public List<string> SkuIds { get; set; } = new();
}

public class SearchListingsRequest
{
    public int BatchNo { get; set; } = 1;
    public string? InternalState { get; set; }
    public string? SearchText { get; set; }
}

public class GetAllListingsRequest
{
    public string? ListingStatus { get; set; }
    public string? PageId { get; set; }
}

// ── Update Price / Inventory ─────────────────────────────────────────────────

public class UpdatePriceRequest
{
    [Required] public Dictionary<string, SkuPriceData> Prices { get; set; } = new();
}

public class SkuPriceData
{
    public string? ProductId { get; set; }
    public ListingPrice? Price { get; set; }
}

public class UpdateInventoryRequest
{
    [Required] public Dictionary<string, SkuInventoryData> Inventories { get; set; } = new();
}

public class SkuInventoryData
{
    public string? ProductId { get; set; }
    public List<ListingLocation>? Locations { get; set; }
}

// ── Product Search ────────────────────────────────────────────────────────────

public class ProductSearchRequest
{
    [Required] public string SearchText { get; set; } = string.Empty;
    public int BatchNo { get; set; } = 1;
}

// ── Responses ─────────────────────────────────────────────────────────────────

public class ListingOperationResponse
{
    public Dictionary<string, SkuOperationResult>? Results { get; set; }
}

public class SkuOperationResult
{
    public string? Status { get; set; }
    public List<ListingError>? Errors { get; set; }
    public List<ListingAttributeError>? AttributeErrors { get; set; }
}

public class ListingError
{
    public string? Severity { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
}

public class ListingAttributeError : ListingError
{
    public string? Attribute { get; set; }
    public string? Path { get; set; }
}

public class GetListingsResponse
{
    public object? Available { get; set; }
    public List<string>? Unavailable { get; set; }
    public List<string>? Invalid { get; set; }
}

public class SearchListingsResponse
{
    public int TotalListingsCount { get; set; }
    public List<object>? ListingData { get; set; }
}

public class GetAllListingsResponse
{
    public string? NextPageId { get; set; }
    public bool HasMore { get; set; }
    public List<object>? Listings { get; set; }
}
