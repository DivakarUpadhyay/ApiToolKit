using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopee.Merchant;

public class GetMerchantInfoRequest
{
    [Required] public long MerchantId { get; set; }
}

public class GetShopsByMerchantRequest
{
    [Required] public long MerchantId { get; set; }
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class MerchantInfoResponse
{
    public long MerchantId { get; set; }
    public string MerchantName { get; set; } = string.Empty;
    public string MerchantStatus { get; set; } = string.Empty;
    public string MerchantDescription { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class ShopsByMerchantResponse
{
    public List<MerchantShopResponse> ShopList { get; set; } = new();
    public bool More { get; set; }
    public int TotalCount { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class MerchantShopResponse
{
    public long ShopId { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
