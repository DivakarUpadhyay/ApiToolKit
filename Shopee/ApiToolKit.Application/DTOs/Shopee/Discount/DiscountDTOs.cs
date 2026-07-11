using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopee.Discount;

public class AddDiscountRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public string DiscountName { get; set; } = string.Empty;
    [Required] public long StartTime { get; set; }
    [Required] public long EndTime { get; set; }
    [Required] public List<DiscountItemRequest> ItemList { get; set; } = new();
}

public class DiscountItemRequest
{
    [Required] public long ItemId { get; set; }
    [Required] public decimal ItemPromotionPrice { get; set; }
    public long ModelId { get; set; } = 0;
    public decimal? ModelPromotionPrice { get; set; }
    public decimal? PurchaseLimit { get; set; }
}

public class UpdateDiscountRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long DiscountId { get; set; }
    public string? DiscountName { get; set; }
    public long? StartTime { get; set; }
    public long? EndTime { get; set; }
}

public class GetDiscountRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long DiscountId { get; set; }
}

public class GetDiscountListRequest
{
    [Required] public long ShopId { get; set; }
    public string DiscountStatus { get; set; } = "upcoming"; // upcoming, ongoing, expired
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class EndDiscountRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long DiscountId { get; set; }
}

public class DeleteDiscountRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long DiscountId { get; set; }
}

public class AddDiscountItemRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long DiscountId { get; set; }
    [Required] public List<DiscountItemRequest> ItemList { get; set; } = new();
}

public class DeleteDiscountItemRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long DiscountId { get; set; }
    [Required] public List<long> ItemIdList { get; set; } = new();
}

public class UpdateDiscountItemRequest
{
    [Required] public long ShopId { get; set; }
    [Required] public long DiscountId { get; set; }
    [Required] public List<DiscountItemRequest> ItemList { get; set; } = new();
}

public class DiscountResponse
{
    public long DiscountId { get; set; }
    public string DiscountName { get; set; } = string.Empty;
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<DiscountItemResponse> ItemList { get; set; } = new();
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class DiscountItemResponse
{
    public long ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal ItemOriginalPrice { get; set; }
    public decimal ItemPromotionPrice { get; set; }
    public long ModelId { get; set; }
}

public class DiscountListResponse
{
    public List<DiscountResponse> DiscountList { get; set; } = new();
    public bool More { get; set; }
    public int TotalCount { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
