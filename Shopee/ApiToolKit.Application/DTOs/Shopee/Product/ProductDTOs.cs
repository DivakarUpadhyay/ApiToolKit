using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopee.Product;

public class GetItemListRequest
{
    [Required] public long ShopId { get; set; }
    public int Offset { get; set; } = 0;
    public int PageSize { get; set; } = 50;
    public string ItemStatus { get; set; } = "NORMAL";
    public long UpdateTimeFrom { get; set; }
    public long UpdateTimeTo { get; set; }
}

public class GetItemBaseInfoRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public List<long> ItemIdList { get; set; } = new();
    public bool NeedTaxInfo { get; set; } = false;
    public bool NeedComplaintPolicy { get; set; } = false;
}

public class AddItemRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string OriginalPrice { get; set; } = string.Empty;
    [Required] public string Description { get; set; } = string.Empty;
    [Required] public string ItemName { get; set; } = string.Empty;
    [Required] public long CategoryId { get; set; }
    [Required] public string Condition { get; set; } = "NEW";
    [Required] public int Stock { get; set; }
    public string Weight { get; set; } = string.Empty;
    public List<string> ImageUrlList { get; set; } = new();
    public List<AttributeRequest> AttributeList { get; set; } = new();
    public LogisticsRequest? Logistics { get; set; }
    public DimensionRequest? Dimension { get; set; }
}

public class AttributeRequest
{
    public long AttributeId { get; set; }
    public List<AttributeValueRequest> AttributeValueList { get; set; } = new();
}

public class AttributeValueRequest
{
    public long ValueId { get; set; }
    public string OriginalValueName { get; set; } = string.Empty;
}

public class LogisticsRequest
{
    public long LogisticId { get; set; }
    public bool Enabled { get; set; }
    public decimal ShippingFee { get; set; }
    public bool IsFree { get; set; }
    public string Size { get; set; } = string.Empty;
}

public class DimensionRequest
{
    public int PackageLength { get; set; }
    public int PackageWidth { get; set; }
    public int PackageHeight { get; set; }
}

public class UpdateItemRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long ItemId { get; set; }
    public string? Description { get; set; }
    public string? ItemName { get; set; }
    public long? CategoryId { get; set; }
    public string? Condition { get; set; }
    public List<string>? ImageUrlList { get; set; }
}

public class UpdatePriceRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long ItemId { get; set; }
    [Required] public decimal Price { get; set; }
    public long ModelId { get; set; } = 0;
}

public class UpdateStockRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long ItemId { get; set; }
    [Required] public int Stock { get; set; }
    public long ModelId { get; set; } = 0;
    public long LocationId { get; set; } = 0;
}

public class DeleteItemRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public List<long> ItemIdList { get; set; } = new();
}

public class GetCategoryRequest
{
    [Required] public long ShopId { get; set; }
    public string Language { get; set; } = "en";
}

public class GetAttributesRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long CategoryId { get; set; }
    public string Language { get; set; } = "en";
}

public class ItemResponse
{
    public long ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public string Condition { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal OriginalPrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public int Stock { get; set; }
    public List<string> Images { get; set; } = new();
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string RequestId { get; set; } = string.Empty;
}

public class ItemListResponse
{
    public List<long> ItemIdList { get; set; } = new();
    public bool HasNextPage { get; set; }
    public int NextOffset { get; set; }
    public int TotalCount { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
