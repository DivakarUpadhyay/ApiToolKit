using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Flipkart.HyperLocal;

public class CreateHyperLocalListingRequest
{
    [Required] public Dictionary<string, HyperLocalSkuData> Listings { get; set; } = new();
}

public class UpdateHyperLocalListingRequest
{
    [Required] public Dictionary<string, HyperLocalSkuData> Listings { get; set; } = new();
}

public class HyperLocalSkuData
{
    public string? ProductId { get; set; }
    public HLPrice? Price { get; set; }
    public HLTax? Tax { get; set; }
    public string? ListingStatus { get; set; }
    public HLPackage? Package { get; set; }
    public HLLocation? Location { get; set; }
    public HLAddressLabel? AddressLabel { get; set; }
    public HLDatingLabel? DatingLabel { get; set; }
    public string? ArchivedStatus { get; set; }
}

public class HLPrice { public int Mrp { get; set; } public int SellingPrice { get; set; } public string Currency { get; set; } = "INR"; }
public class HLTax { public string? Hsn { get; set; } public string? TaxCode { get; set; } }
public class HLPackage { public string? Name { get; set; } public HLDimensions? Dimensions { get; set; } public decimal Weight { get; set; } public string? Description { get; set; } }
public class HLDimensions { public decimal Length { get; set; } public decimal Breadth { get; set; } public decimal Height { get; set; } }
public class HLLocation { [Required] public string Id { get; set; } = string.Empty; public string? Status { get; set; } public int Inventory { get; set; } }
public class HLAddressLabel { public List<string>? ManufacturerDetails { get; set; } public List<string>? ImporterDetails { get; set; } public List<string>? CountriesOfOrigin { get; set; } }
public class HLDatingLabel { public long? MfgDate { get; set; } public long? ShelfLife { get; set; } }

public class UpdateHLPriceRequest { [Required] public Dictionary<string, HLSkuPriceData> Prices { get; set; } = new(); }
public class HLSkuPriceData { public string? ProductId { get; set; } public HLPrice? Price { get; set; } }

public class UpdateHLInventoryRequest { [Required] public Dictionary<string, HLSkuInventoryData> Inventories { get; set; } = new(); }
public class HLSkuInventoryData { public string? ProductId { get; set; } public List<HLLocation>? Locations { get; set; } }

public class GetHLListingRequest { [Required] public string SkuIds { get; set; } = string.Empty; }

public class HyperLocalOperationResponse { public Dictionary<string, object>? Results { get; set; } }
