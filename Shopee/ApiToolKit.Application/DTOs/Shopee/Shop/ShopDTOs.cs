using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopee.Shop;

public class ShopRequest
{
    [Required] public long ShopId { get; set; }
}

public class UpdateShopProfileRequest
{
    [Required] public long ShopId { get; set; }
    public string? ShopName { get; set; }
    public string? ShopDescription { get; set; }
    public string? Avatar { get; set; }
}

public class ShopInfoResponse
{
    public long ShopId { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string ShopStatus { get; set; } = string.Empty;
    public string ShopDescription { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public long OpenTime { get; set; }
    public bool IsOfficialShop { get; set; }
    public bool IsCbOption { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string RequestId { get; set; } = string.Empty;
}

public class ShopListResponse
{
    public List<ShopInfoResponse> ShopList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
}
