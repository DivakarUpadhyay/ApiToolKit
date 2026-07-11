namespace ApiToolKit.Application.DTOs.Shopee.Public;

public class GetShopsByPartnerRequest
{
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class PartnerShopInfo
{
    public long ShopId { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

public class GetShopsByPartnerResponse
{
    public List<PartnerShopInfo> ShopList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class GetCategoriesByCountryRequest
{
    public string Country { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
}

public class CategoryInfo
{
    public long CategoryId { get; set; }
    public long ParentCategoryId { get; set; }
    public string OriginalCategoryName { get; set; } = string.Empty;
    public string DisplayCategoryName { get; set; } = string.Empty;
    public bool HasChildren { get; set; }
}

public class GetCategoriesByCountryResponse
{
    public List<CategoryInfo> CategoryList { get; set; } = new();
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class GetPaymentMethodListRequest
{
    public string Region { get; set; } = string.Empty;
}

public class PaymentMethodInfo
{
    public int PaymentMethodId { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
}

public class GetPaymentMethodListResponse
{
    public List<PaymentMethodInfo> PaymentMethodList { get; set; } = new();
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
